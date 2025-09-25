using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RSW.Shared.Entities
{
    public class Association : BaseEntity
    {
        public required string Name { get; set; }
        public string Abbreviation { get; set; } = string.Empty;
        [JsonIgnore]
        public List<Group> Groups { get; set; } = new List<Group>();
    }
}
