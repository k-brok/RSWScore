using System.ComponentModel.DataAnnotations.Schema;

namespace RSW.Shared.Entities
{
    public class Patrol : BaseEntity
    {
        public required string Name { get; set; }
        public int? Number { get; set; } = null;
        public Guid? SubGroupId { get; set; } = null;
        public SubGroup? SubGroup { get; set; } = null;
        public Guid GroupId { get; set; }
        public Group Group { get; set; } = null!;
        public List<Score> Scores { get; set; } = new List<Score>();
        public decimal? TotalScore { get; set; } = null;
        public int? position { get; set; } = null;
        [NotMapped] public List<string> DisqualifiedMessages { get; set; } = new List<string>();
        public bool IsYoungest { get; set; } = false;
        public List<Scout> Scouts { get; set; } = new List<Scout>();
        
    }
}
