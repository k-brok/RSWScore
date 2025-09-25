using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RSW.Shared.Entities
{
    public class Patrol : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public int? Number { get; set; } = null;
        public Guid? SubGroupId { get; set; } = null;
        [JsonIgnore]
        public SubGroup? SubGroup { get; set; } = null;
        public Guid GroupId { get; set; }
        public Group Group { get; set; } = null!;
        [JsonIgnore]
        public List<Score> Scores { get; set; } = new List<Score>();
        public decimal? TotalScore { get; set; } = null;
        public int? position { get; set; } = null;
        [JsonIgnore]
        [NotMapped] public List<string> DisqualifiedMessages { get; set; } = new List<string>();
        public bool IsYoungest { get; set; } = false;
        [JsonIgnore]
        public List<Scout> Scouts { get; set; } = new List<Scout>();
        public bool unranked(DateOnly LSWDate)
        {
            if (Scouts.Count < 5)
                return true;

            if (Scouts.Count > 7)
                return true;

            int NrScoutsAge15 = 0;

            foreach (Scout scout in Scouts)
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
