using RSW.Shared.Entities;

namespace RSW.Shared.Dto
{
    public class CriteriaDto : BaseEntityDto
    {
        public string? Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public int MaxScore { get; set; }
        public int SubCategoryId { get; set; }
    }
    public static class CriteriaExtensions
    {
        public static CriteriaDto ToDto(this Criteria criteria)
        {
            return new CriteriaDto
            {
                Id = criteria.Id,
                Name = criteria.Name,
                Description = criteria.Description,
                SubCategoryId = criteria.SubCategoryId,
                MaxScore = criteria.MaxScore
            };
        }

        public static Criteria ToEntity(this CriteriaDto criteria)
        {
            return new Criteria
            {
                Id = criteria.Id,
                Name = criteria.Name,
                Description = criteria.Description,
                SubCategoryId = criteria.SubCategoryId,
                MaxScore = criteria.MaxScore
            };
        }
    }
}
