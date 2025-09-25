using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RSW.Shared.Entities
{
    public class Scout : BaseEntity
    {
        [Required]
        public string? Firstname { get; set; }
        [Required]
        public string? Lastname { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public bool IsPL { get; set; } = false;
        public bool IsAPL { get; set; } = false;
        [JsonIgnore]
        public Patrol Patrol { get; set; } = null!;
        [Required]
        public Guid PatrolId { get; set; }
        
    }
}
