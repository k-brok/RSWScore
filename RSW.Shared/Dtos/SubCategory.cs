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

    // Voor bijwerken
    public class SubCategoryUpdateDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public Guid CategoryId { get; set; }
    }

    // Voor teruggeven aan de client
    public class SubCategoryReadDto : BaseEntityDto
    {
        public string Name { get; set; } = string.Empty;

        public Guid CategoryId { get; set; }

        public List<CriteriaReadDto> Criterias { get; set; } = new();
    }
}
