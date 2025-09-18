namespace RSW.Shared.Dto
{
    public class CriteriaDto : BaseEntityDto
    {
        public string? Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public int MaxScore { get; set; }
        public int SubCategoryId { get; set; }
    }
}
