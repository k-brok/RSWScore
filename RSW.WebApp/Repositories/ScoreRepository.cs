using System.Xml.Linq;
using Microsoft.EntityFrameworkCore;
using RSW.WebApp.Data;
using RSW.WebApp.Entities;
using RSW.WebApp.Interface.Repositories;

namespace RSW.WebApp.Repositories
{
    public class ScoreRepository : IScoreRepository
    {
        private readonly ApplicationDbContext _Context;
        public ScoreRepository(ApplicationDbContext applicationDbContext)
        {
            _Context = applicationDbContext;
        }
        public async Task<List<Score>> GetAllAsync()
        {
            return await _Context.Scores.ToListAsync();
        }
        public async Task<Score> GetAsync(int Id)
        {
            return await _Context.Scores.FirstOrDefaultAsync(S => S.Id == Id);
        }
        public async Task<Score> GetAsync(Patrol patrol, Criteria criteria)
        {
            Score FindScore = await _Context.Scores.FirstOrDefaultAsync(A => A.PatrolId == patrol.Id && A.CriteriaId == criteria.Id);
            if (FindScore == null)
            {
                FindScore = new Score();
                FindScore.PatrolId = patrol.Id;
                FindScore.CriteriaId = criteria.Id;
            }

            return FindScore;
        }

        public async Task<List<Score>> GetAsync(Patrol patrol)
        {
            return  await _Context.Scores.Where(A => A.PatrolId == patrol.Id).ToListAsync();
        }

        public async Task<List<Score>> GetAsync(Patrol patrol, Category category)
        {
            List<int> CategoryCriteriaId = category.SubCategories.SelectMany(S => S.criterias).Select(C => C.Id).ToList();
            return await _Context.Scores.Where(A => A.PatrolId == patrol.Id && CategoryCriteriaId.Contains(A.CriteriaId)).ToListAsync();
        }

        public async Task Save(Score score)
        {
            if(score.Id == 0)
            {
                Score CurrentScore = await _Context.Scores.FirstOrDefaultAsync(S => S.PatrolId == score.PatrolId && S.CriteriaId == score.CriteriaId);
                if(CurrentScore == null)
                {
                    _Context.Scores.Add(score);
                }
                else
                {
                    CurrentScore.Value = score.Value;
                }
            }
            else
            {
                Score CurrentScore = await _Context.Scores.FirstOrDefaultAsync(S => S.Id == score.Id);
                CurrentScore.Value = score.Value;
            }

            await _Context.SaveChangesAsync();
        }
        public async Task RevertEdits(Score score)
        {
            var scoreEntry = _Context.Entry(score);
            if (scoreEntry.State == EntityState.Modified)
            {
                scoreEntry.CurrentValues.SetValues(scoreEntry.OriginalValues);
                scoreEntry.State = EntityState.Unchanged;
            }
        }
        public async Task Delete(Score score)
        {
            try
            {
                Score DeleteScore = await _Context.Scores.FirstOrDefaultAsync(S => S.Id == score.Id);
                _Context.Scores.Remove(DeleteScore);
                await _Context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
