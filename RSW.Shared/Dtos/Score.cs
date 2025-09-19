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
    public class ScoreUpdateDto
    {
        [Required]
        public Guid PatrolId { get; set; }

        [Required]
        public Guid CriteriaId { get; set; }

        public int Value { get; set; } = 0;
    }

    // Voor terugsturen naar de client
    public class ScoreReadDto : BaseEntityDto
    {
        public Guid PatrolId { get; set; }

        public Guid CriteriaId { get; set; }

        public int Value { get; set; } = 0;
    }
}
