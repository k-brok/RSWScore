using RSW.Shared.Entities;

namespace RSW.Shared.Dto
{
    public class CategoryDto : BaseEntityDto
    {
        public required string Name { get; set; }
        public int Weight { get; set; }
        public List<SubCategoryDto> SubCategories { get; set; } = new List<SubCategoryDto>();
    }
    public static class CategoryExtensions
    {
        public static CategoryDto ToDto(this Category category)
        {
            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Weight = category.Weight,
                SubCategories = category.SubCategories.Select(sc => sc.ToDto()).ToList()
            };
        }

        public static Category ToEntity(this CategoryDto category)
        {
            return new Category
            {
                Id = category.Id,
                Name = category.Name,
                Weight = category.Weight,
                SubCategories = category.SubCategories.Select(sc => sc.ToEntity()).ToList()
            };
        }
    }
}
