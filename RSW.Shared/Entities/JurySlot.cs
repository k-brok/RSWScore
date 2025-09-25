using System.Text.Json.Serialization;

namespace RSW.Shared.Entities
{
    public class JurySlot : BaseEntity
    {
        public DateTime OpeningTime { get; set; } = DateTime.UtcNow;
        public DateTime ClosingTime { get; set; } = DateTime.UtcNow.AddHours(2);
        [JsonIgnore]
        public Category Category { get; set; } = null!;
        public Guid CategoryId { get; set; }
        [JsonIgnore]
        public SubGroup SubGroup { get; set; } = null!;
        public Guid SubgroupId { get; set; }
    }
}
