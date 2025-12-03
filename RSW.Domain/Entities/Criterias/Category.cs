using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RSW.Domain.Entities
{
    public class Category : BaseEntity
    {
        [Required]
        public string? Name { get; set; }
        public int Weight { get; set; }
        [JsonIgnore]
        public List<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
        [JsonIgnore]
        public List<JurySlot> JurySlots { get; set; } = new List<JurySlot>();
        [NotMapped] public int MaxScore { get
            {
                return SubCategories.SelectMany(S => S.criterias).Select(C => C.MaxScore).Sum();
            }
        }

    }
}
