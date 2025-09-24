using System.ComponentModel.DataAnnotations;

namespace RSW.Shared.Dto
{
    // Voor aanmaken
    public class PatrolCreateDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public Guid GroupId { get; set; }
        public bool IsYoungest { get; set; } = false;
    }

    public class PatrolDto : BaseEntityDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public int? Number { get; set; }
        public Guid? SubGroupId { get; set; }
        [Required]
        public Guid GroupId { get; set; }
        public decimal? TotalScore { get; }
        public int? Position { get; }
        public bool IsYoungest { get; set; }
        public List<ScoutDto> Scouts { get; set; } = new();
        public bool unranked(DateOnly LSWDate)
        {
            if (Scouts.Count < 5)
                return true;

            if (Scouts.Count > 7)
                return true;

            int NrScoutsAge15 = 0;

            foreach (ScoutDto scout in Scouts)
            {
                if (scout.CalculateAge(LSWDate) < 11)
                    return true;

                if (scout.CalculateAge(LSWDate) > 15)
                    return true;

                if (scout.CalculateAge(LSWDate) == 15)
                    NrScoutsAge15++;
            }

            if (Scouts.Count == 5 && NrScoutsAge15 > 2)
                return true;
            
            if (NrScoutsAge15 > 3)
                return true;

            return false;
        }
    }
}
