using System.Text.Json.Serialization;

namespace RSW.Shared.Entities
{
    public class Edition : BaseEntity
    {
        public DateOnly RSWStartDate { get; set; }
        public DateOnly? LSWStartDate { get; set; } = null;
        public DateOnly? PreSignupClose { get; set; } = null;
        public int Year
        {
            get
            {
                return RSWStartDate.Year;
            }
        }
        public String? Theme { get; set; } = string.Empty;
        [JsonIgnore]
        public List<SubGroup> SubGroups { get; set; } = new List<SubGroup>();
        public bool IsActive { get; set; } = false;
        [JsonIgnore]
        public List<JurySlot> JurySlots { get; set; } = new List<JurySlot>();
    }
}
