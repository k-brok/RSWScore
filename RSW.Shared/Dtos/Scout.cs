using System;
using System.ComponentModel.DataAnnotations;

namespace RSW.Shared.Dto
{
    // Voor aanmaken
    public class ScoutCreateDto
    {
        [Required]
        public string Firstname { get; set; } = string.Empty;

        [Required]
        public string Lastname { get; set; } = string.Empty;

        [Required]
        public DateOnly DateOfBirth { get; set; }

        [Required]
        public Guid PatrolId { get; set; }
    }

    // Voor bijwerken
    public class ScoutDto : BaseEntityDto
    {
        [Required]
        public string Firstname { get; set; } = string.Empty;

        [Required]
        public string Lastname { get; set; } = string.Empty;

        [Required]
        public DateOnly DateOfBirth { get; set; }

        public bool IsPL { get; } = false;

        public bool IsAPL { get; } = false;

        [Required]
        public Guid PatrolId { get; set; }
    }
}
