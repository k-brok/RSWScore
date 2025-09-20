using System;
using System.ComponentModel.DataAnnotations;

namespace RSW.Shared.Dto
{
    public class SubGroupCreateDto
    {
        [Required]
        public string Color { get; set; } = string.Empty;

        [Required]
        public Guid EditionId { get; set; }
    }

    public class SubGroupDto : BaseEntityDto
    {
        [Required]
        public string Color { get; set; } = string.Empty;

        [Required]
        public Guid EditionId { get; set; }
    }
}
