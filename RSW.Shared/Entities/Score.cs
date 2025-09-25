using System.Text.Json.Serialization;

namespace RSW.Shared.Entities
{
    public class Score : BaseEntity
    {
        public Patrol Patrol { get; set; } = null!;
        public Guid PatrolId { get; set; }
        public Guid CriteriaId { get; set; }
        [JsonIgnore]
        public Criteria Criteria { get; set; } = null!;
        public int Value { get; set; } = 0;
        
    }
}
