using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RSW.Domain.Entities
{
    public class Association : BaseEntity
    {
        [Required]
        public string? Name { get; set; }
        public string Abbreviation { get; set; } = string.Empty;
        [JsonIgnore]
        public List<Unit> Groups { get; set; } = new List<Unit>();
    }
}
