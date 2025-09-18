using System.ComponentModel.DataAnnotations.Schema;

namespace RSW.Shared.Entities
{
    public class Category : BaseEntity
    {
        public required string Name { get; set; }
        public int Weight { get; set; }
        public List<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
        public List<JurySlot> JurySlots { get; set; } = new List<JurySlot>();
        [NotMapped] public int MaxScore { get
            {
                return SubCategories.SelectMany(S => S.criterias).Select(C => C.MaxScore).Sum();
            }
        }

    }
}
