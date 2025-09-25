using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RSW.Shared.Entities
{
    public class Scout : BaseEntity
    {
        public required string Firstname { get; set; }
        public required string Lastname { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public bool IsPL { get; set; } = false;
        public bool IsAPL { get; set; } = false;
        [JsonIgnore]
        public Patrol Patrol { get; set; } = null!;
        public required Guid PatrolId { get; set; }
        
    }
}
