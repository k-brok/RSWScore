using System.ComponentModel.DataAnnotations.Schema;
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
        [JsonIgnore]
        [NotMapped]
        public bool BoolValue
        {
            get => Value == 1;
            set => Value = value ? 1 : 0;
        }
        
    }
}
