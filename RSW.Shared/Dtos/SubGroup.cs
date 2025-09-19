using System;
using System.ComponentModel.DataAnnotations;

namespace RSW.Shared.Dto
{
    // Voor aanmaken
    public class SubGroupCreateDto
    {
        [Required]
        public string Color { get; set; } = string.Empty;

        [Required]
        public Guid EditionId { get; set; }
    }

    // Voor bijwerken
    public class SubGroupUpdateDto
    {
        [Required]
        public string Color { get; set; } = string.Empty;

        [Required]
        public Guid EditionId { get; set; }
    }

    // Voor terugsturen naar de client
    public class SubGroupReadDto : BaseEntityDto
    {
        public string Color { get; set; } = string.Empty;

        public Guid EditionId { get; set; }
    }
}
