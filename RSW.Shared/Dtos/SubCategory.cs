using System.ComponentModel.DataAnnotations;

namespace RSW.Shared.Dto
{
    // Voor aanmaken
    public class SubCategoryCreateDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public Guid CategoryId { get; set; }
    }

    public class SubCategoryDto : BaseEntityDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public Guid CategoryId { get; set; }

        public List<CriteriaDto> Criterias { get; set; } = new();
    }
}
