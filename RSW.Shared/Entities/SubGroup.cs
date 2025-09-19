namespace RSW.Shared.Entities
{
    public class SubGroup : BaseEntity
    {
        public required string Color { get; set; }
        public required Guid EditionId { get; set; }
        public Edition Edition { get; set; } = null!;
        public List<Patrol> patrols { get; set; } = new List<Patrol>();
        public List<JurySlot> JurySlots { get; set; } = new List<JurySlot>();
    }
}
