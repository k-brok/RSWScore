namespace RSW.WebApp.Entities
{
    public class Edition : BaseEntity
    {
        public DateOnly RSWStartDate { get; set; }
        public DateOnly? LSWStartDate { get; set; } = null;
        public int Year { get
            {
                return RSWStartDate.Year;
            } }
        public String? Theme { get; set; } = string.Empty;
        public ICollection<SubGroup> SubGroups { get; set; } = new List<SubGroup>();
        public bool IsActive { get; set; } = false;
        public List<JurySlot> JurySlots { get; set; }
    }
}
