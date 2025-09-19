using System;
using System.ComponentModel.DataAnnotations;

namespace RSW.Shared.Dto
{
    public class JurySlotCreateDto
    {
        public DateTime OpeningTime { get; set; } = DateTime.UtcNow;

        public DateTime ClosingTime { get; set; } = DateTime.UtcNow.AddHours(2);

        [Required]
        public string Code { get; set; } = string.Empty;

        [Required]
        public Guid CategoryId { get; set; }

        [Required]
        public Guid SubgroupId { get; set; }

        [Required]
        public Guid EditionId { get; set; }
    }
    public class JurySlotUpdateDto
    {
        public DateTime OpeningTime { get; set; } = DateTime.UtcNow;

        public DateTime ClosingTime { get; set; } = DateTime.UtcNow.AddHours(2);

        [Required]
        public string Code { get; set; } = string.Empty;

        [Required]
        public Guid CategoryId { get; set; }

        [Required]
        public Guid SubgroupId { get; set; }

        [Required]
        public Guid EditionId { get; set; }
    }
    public class JurySlotReadDto : BaseEntityDto
    {
        public DateTime OpeningTime { get; set; }
        public DateTime ClosingTime { get; set; }
        public string Code { get; set; } = string.Empty;
        public Guid CategoryId { get; set; }
        public Guid SubgroupId { get; set; }
        public Guid EditionId { get; set; }
    }
}
