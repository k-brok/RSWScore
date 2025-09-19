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

    // Voor bijwerken
    public class CategoryUpdateDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public int Weight { get; set; } = 0;
    }

    // Voor teruggeven aan de client
    public class CategoryReadDto : BaseEntityDto
    {
        public string Name { get; set; } = string.Empty;

        public int Weight { get; set; } = 0;

        public List<SubCategoryReadDto> SubCategories { get; set; } = new();
    }
}
