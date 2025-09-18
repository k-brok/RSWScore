using System.ComponentModel.DataAnnotations.Schema;

namespace RSW.Shared.Dto
{
    public class ScoutDto : BaseEntityDto
    {
        public required string Firstname { get; set; }
        public required string Lastname { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public bool IsPL { get; set; } = false;
        public bool IsAPL { get; set; } = false;
        public required Guid PatrolId { get; set; }
    }
}
