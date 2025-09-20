using System.ComponentModel.DataAnnotations;

namespace RSW.Shared.Dto
{
    public class CriteriaCreateDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int MaxScore { get; set; } = 0;

        [Required]
        public Guid SubCategoryId { get; set; }
    }
    public class CriteriaDto : BaseEntityDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int MaxScore { get; set; } = 0;
        [Required]
        public Guid SubCategoryId { get; set; }
    }
}
