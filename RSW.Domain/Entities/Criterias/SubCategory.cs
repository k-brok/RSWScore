using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RSW.Domain.Entities
{
    public class SubCategory : BaseEntity
    {
        [Required]
        public string? Name { get; set; }
        [JsonIgnore]
        public List<Criteria> criterias { get; set; } = new List<Criteria>();
        [Required]
        public Guid CategoryId { get; set; }
        [JsonIgnore]
        public Category Category { get; set; } = null!;
    }
}
