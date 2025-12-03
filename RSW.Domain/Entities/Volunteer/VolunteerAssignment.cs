using System.Text.Json.Serialization;

namespace RSW.Domain.Entities
{
    public class VolunteerAssignment : BaseEntity
    {
        [JsonIgnore]
        public ApplicationUser? User { get; set; }
        public string? UserId { get; set; }
        [JsonIgnore]
        public Edition? Edition { get; set; }
        public Guid EditionId { get; set; }
        [JsonIgnore]
        public VolunteerTask? Task { get; set; }
        public Guid TaskId { get; set; }
        public bool HasVOG { get; set; } = false;
        
    }
}
