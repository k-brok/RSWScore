namespace RSW.WebApp.Entities
{
    public class Criteria : BaseEntity
    {
        public string? Name { get; set; }
        public string Description { get; set; }
        public int MaxScore { get; set; }
        public int SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; }
        public List<Score> Scores { get; set; }
    }
}
