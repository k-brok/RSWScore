using System.Text.Json.Serialization;

namespace RSW.Shared.Entities
{
    public class Group : BaseEntity
    {
        public required string Name { get; set; }
        public Guid AssociationId { get; set; }
        [JsonIgnore]
        public Association Association { get; set; } = null!;
        [JsonIgnore]
        public List<Patrol> Patrols { get; set; } = new List<Patrol>();
    }
}
