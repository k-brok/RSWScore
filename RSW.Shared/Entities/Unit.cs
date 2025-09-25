using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RSW.Shared.Entities
{
    public class Unit : BaseEntity
    {
        [Required]
        public string? Name { get; set; }
        public Guid AssociationId { get; set; }
        [JsonIgnore]
        public Association Association { get; set; } = null!;
        [JsonIgnore]
        public List<Patrol> Patrols { get; set; } = new List<Patrol>();
    }
}
