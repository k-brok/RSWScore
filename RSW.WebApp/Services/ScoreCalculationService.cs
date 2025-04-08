using RSW.WebApp.Entities;
using RSW.WebApp.Repositories;

namespace RSW.WebApp.Services
{
    public class ScoreCalculationService
    {
        private readonly CurrentEditionService _current;
        public ScoreCalculationService(CurrentEditionService currentEditionService)
        {
            _current = currentEditionService;
        }
        public async Task<int> CalculatePoints(Patrol patrol)
        {
            int CalcPoints = 0;

            foreach (Score score in patrol.Scores)
            {
                CalcPoints += score.Value;
            }

            return CalcPoints;
        }
        public async Task<int> CalculatePoints(Patrol patrol, Category category)
        {
            int CalcPoints = 0;
            List<int> CriteriaIds = category.SubCategories.SelectMany(S => S.criterias).Select(C => C.Id).ToList();
            if (patrol.Scores.Any())
            {
                foreach (Score score in patrol.Scores.Where(S => CriteriaIds.Contains(S.CriteriaId)))
                {
                    CalcPoints += score.Value;
                }
            }

            return CalcPoints;
        }
        public async Task<decimal> CalculatePercentages(Patrol patrol, Category category)
        {
            int CalcPoints = await CalculatePoints(patrol, category);
            int MaxPoints = category.MaxScore;
            decimal Percentage = ((decimal)CalcPoints / (decimal)MaxPoints) * 100;
            return Percentage;
        }
        public async Task<decimal> CalculateTotalPercentages(Patrol patrol, Category category)
        {
            decimal CategoryScore = await CalculatePercentages(patrol, category);

            return CategoryScore * ((decimal)category.Weight / 100);
        }
        public async Task<decimal> CalculateTotalPercentages(Patrol patrol, List<Category> categories)
        {

            decimal TotalScore = 0;

            foreach(Category category in categories)
            {
                TotalScore += await CalculateTotalPercentages(patrol, category);
            }

            patrol.TotalScore = TotalScore;

            return TotalScore;
        }
        public async Task DetermainPosition(Edition edition)
        {
            // Stap 1: Sorteer aflopend op Value
            List<Patrol> patrols = edition.SubGroups.SelectMany(S => S.patrols).OrderByDescending(x => x.TotalScore).ToList();

            // Stap 2: Pak de eerste twee geldige items
            List<Patrol> firstTwoValid = patrols.Where(x => x.IsDisqualified == false).Take(2).ToList();

            // Stap 3: Maak een nieuwe lijst zonder deze twee (maar behoud de originele volgorde)
            List<Patrol> remainingItems = new List<Patrol>(patrols);
            foreach (var validItem in firstTwoValid)
            {
                remainingItems.Remove(validItem);
            }

            // Stap 4: Voeg de eerste twee geldige bovenaan de lijst toe
            List<Patrol> sortedItems = new List<Patrol>();
            sortedItems.AddRange(firstTwoValid);
            sortedItems.AddRange(remainingItems);

            int CurrentPosition = 0;
            decimal? PreviousScore = 101;
            // Print het resultaat
            foreach (Patrol patrol in sortedItems)
            {
                if (PreviousScore != patrol.TotalScore)
                {
                    CurrentPosition++;
                    PreviousScore = patrol.TotalScore;
                }

                patrol.position = CurrentPosition;

            }
        }
        public async Task<Edition> UpdateEdition(Edition edition,List<Category> Categories)
        {
            List<Patrol> patrols = edition.SubGroups.SelectMany(S => S.patrols).ToList();

            // Start alle taken tegelijk
            var tasks = patrols.Select(patrol => CalculateTotalPercentages(patrol, Categories));

            // Wachten tot alle taken klaar zijn
            await Task.WhenAll(tasks);

            await DetermainPosition(edition);
            return edition;
        }
        public async Task<Edition> UpdateEdition()
        {

            // Start alle taken tegelijk
            var tasks = _current.Edition.SubGroups.SelectMany(P => P.patrols).Select(patrol => CalculateTotalPercentages(patrol, _current.Categories));

            // Wachten tot alle taken klaar zijn
            await Task.WhenAll(tasks);

            await DetermainPosition(_current.Edition);
            return _current.Edition;
        }
    }
}
