using System.ComponentModel.DataAnnotations.Schema;

namespace RSW.WebApp.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public int Weight { get; set; }
        public List<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
        public List<JurySlot> JurySlots { get; set; }
        [NotMapped] public int MaxScore { get
            {
                int Score = SubCategories.SelectMany(S => S.criterias).Select(C => C.MaxScore).Sum();
                if(Score != null)
                {
                    return Score;
                }
                return 0;
            }
        }

    }
}
