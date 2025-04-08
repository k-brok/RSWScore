namespace RSW.WebApp.Entities
{
    public class SubGroup : BaseEntity
    {
        public string Color { get; set; }
        public int EditionId { get; set; }
        public Edition Edition { get; set; }
        public List<Patrol> patrols { get; set; }
        public List<JurySlot> JurySlots { get; set; }
    }
}
