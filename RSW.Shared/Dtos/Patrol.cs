using System.ComponentModel.DataAnnotations;

namespace RSW.Shared.Dto
{
    // Voor aanmaken
    public class PatrolCreateDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public Guid GroupId { get; set; }
        public bool IsYoungest { get; set; } = false;
    }
    public class PatrolUpdateDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public int? Number { get; set; }
        public Guid? SubGroupId { get; set; }
        [Required]
        public Guid GroupId { get; set; }
        public bool IsYoungest { get; set; } = false;
    }

    public class PatrolReadDto : BaseEntityDto
    {
        public string Name { get; set; } = string.Empty;
        public int? Number { get; set; }
        public Guid? SubGroupId { get; set; }
        public Guid GroupId { get; set; }
        public decimal? TotalScore { get; set; }
        public int? Position { get; set; }
        public bool IsYoungest { get; set; }
    }
}
