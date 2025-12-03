using System.Text.Json.Serialization;

namespace RSW.Domain.Entities
{
    public class Criteria : BaseEntity
    {
        public string? Name { get; set; }
        public string Description { get; set; } = string.Empty;
        public int MaxScore { get; set; }
        public Guid SubCategoryId { get; set; }
        [JsonIgnore]
        public SubCategory SubCategory { get; set; } = null!;
        [JsonIgnore]
        public List<Score> Scores { get; set; } = new List<Score>();
    }
}
