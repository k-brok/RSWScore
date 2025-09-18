using RSW.Shared.Entities;

namespace RSW.Shared.Dto
{
    public class SubCategoryDto : BaseEntityDto
    {
        public required string Name { get; set; }
        public required Guid CategoryId { get; set; }
        public List<CriteriaDto> criterias { get; set; } = new List<CriteriaDto>();
    }
    public static class SubCategoryExtensions
    {
        public static SubCategoryDto ToDto(this SubCategory category)
        {
            return new SubCategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                CategoryId = category.CategoryId,
                criterias = category.criterias.Select(c => c.ToDto()).ToList()
            };
        }

        public static SubCategory ToEntity(this SubCategoryDto category)
        {
            return new SubCategory
            {
                Id = category.Id,
                Name = category.Name,
                CategoryId = category.CategoryId,
                criterias = category.criterias.Select(c => c.ToEntity()).ToList()
            };
        }
    }
}
