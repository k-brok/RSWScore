using System;
using System.ComponentModel.DataAnnotations;

namespace RSW.Shared.Dto
{
    public class JurySlotCreateDto
    {
        public DateTime OpeningTime { get; set; } = DateTime.UtcNow;

        public DateTime ClosingTime { get; set; } = DateTime.UtcNow.AddHours(2);

        [Required]
        public Guid CategoryId { get; set; }

        [Required]
        public Guid SubgroupId { get; set; }
    }
    public class JurySlotDto :BaseEntityDto
    {
        public DateTime OpeningTime { get; set; } = DateTime.UtcNow;

        public DateTime ClosingTime { get; set; } = DateTime.UtcNow.AddHours(2);


        [Required]
        public Guid CategoryId { get; set; }

        [Required]
        public Guid SubgroupId { get; set; }
    }
}
