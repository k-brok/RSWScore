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

        public bool IsPL { get; set; } = false;

        public bool IsAPL { get; set; } = false;

        [Required]
        public Guid PatrolId { get; set; }
    }

    // Voor bijwerken
    public class ScoutUpdateDto
    {
        [Required]
        public string Firstname { get; set; } = string.Empty;

        [Required]
        public string Lastname { get; set; } = string.Empty;

        [Required]
        public DateOnly DateOfBirth { get; set; }

        public bool IsPL { get; set; } = false;

        public bool IsAPL { get; set; } = false;

        [Required]
        public Guid PatrolId { get; set; }
    }

    // Voor terugsturen naar de client
    public class ScoutReadDto : BaseEntityDto
    {
        public string Firstname { get; set; } = string.Empty;

        public string Lastname { get; set; } = string.Empty;

        public DateOnly DateOfBirth { get; set; }

        public bool IsPL { get; set; } = false;

        public bool IsAPL { get; set; } = false;

        public Guid PatrolId { get; set; }
    }
}
