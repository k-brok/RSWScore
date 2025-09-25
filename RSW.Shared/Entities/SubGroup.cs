using System.Text.Json.Serialization;

namespace RSW.Shared.Entities
{
    public class SubGroup : BaseEntity
    {
        public required string Color { get; set; }
        public required Guid EditionId { get; set; }
        [JsonIgnore]
        public Edition Edition { get; set; } = null!;
        [JsonIgnore]
        public List<Patrol> patrols { get; set; } = new List<Patrol>();
        [JsonIgnore]
        public List<JurySlot> JurySlots { get; set; } = new List<JurySlot>();
    }
}
