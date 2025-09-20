using System;
using System.ComponentModel.DataAnnotations;

namespace RSW.Shared.Dto
{
    // Voor aanmaken
    public class ScoreCreateDto
    {
        [Required]
        public Guid PatrolId { get; set; }

        [Required]
        public Guid CriteriaId { get; set; }

        public int Value { get; set; } = 0;
    }

    // Voor bijwerken
    public class ScoreDto : BaseEntityDto
    {
        [Required]
        public Guid PatrolId { get; set; }

        [Required]
        public Guid CriteriaId { get; set; }

        public int Value { get; set; } = 0;
    }
}
