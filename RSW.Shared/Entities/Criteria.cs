namespace RSW.Shared.Entities
{
    public class Criteria : BaseEntity
    {
        public string? Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public int MaxScore { get; set; }
        public int SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; } = null!;
        public List<Score> Scores { get; set; } = new List<Score>();
    }
}
