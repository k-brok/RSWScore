using System.ComponentModel.DataAnnotations;

namespace RSW.Shared.Dto
{
    // Voor aanmaken
    public class CategoryCreateDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public int Weight { get; set; } = 0;
    }
    public class CategoryDto : BaseEntityDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public int Weight { get; set; } = 0;

        public List<SubCategoryDto> SubCategories { get; set; } = new();
    }
}
