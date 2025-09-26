using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RSW.Shared.Entities
{
    public class SubGroup : BaseEntity
    {
        [Required]
        public string? Color { get; set; }
        [Required]
        public Guid EditionId { get; set; }
        [JsonIgnore]
        public Edition Edition { get; set; } = null!;
        [JsonIgnore]
        public IEnumerable<Patrol> Patrols { get; set; } = new List<Patrol>();
        [JsonIgnore]
        public IEnumerable<JurySlot> JurySlots { get; set; } = new List<JurySlot>();
    }
}
