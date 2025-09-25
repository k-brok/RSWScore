using System.Text.Json.Serialization;

namespace RSW.Shared.Entities
{
    public class SubCategory : BaseEntity
    {
        public required string Name { get; set; }
        [JsonIgnore]
        public List<Criteria> criterias { get; set; } = new List<Criteria>();
        public required Guid CategoryId { get; set; }
        [JsonIgnore]
        public Category Category { get; set; } = null!;
    }
}
