namespace RSW.Shared.Entities
{
    public class JurySlot : BaseEntity
    {
        public DateTime OpeningTime { get; set; } = DateTime.UtcNow;
        public DateTime ClosingTime { get; set; } = DateTime.UtcNow.AddHours(2);
        public required string Code { get; set; }
        public Category Category { get; set; } = null!;
        public Guid CategoryId { get; set; }
        public SubGroup SubGroup { get; set; } = null!;
        public Guid SubgroupId { get; set; }
        public Edition Edition { get; set; } = null!;
        public Guid EditionId { get; set; }
    }
}
