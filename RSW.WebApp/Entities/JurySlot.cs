namespace RSW.WebApp.Entities
{
    public class JurySlot : BaseEntity
    {
        public DateTime OpeningTime { get; set; } = DateTime.UtcNow;
        public DateTime ClosingTime { get; set; } = DateTime.UtcNow.AddHours(2);
        public string Code { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public SubGroup SubGroup { get; set; }
        public int SubgroupId { get; set; }
        public Edition Edition { get; set; }
        public int EditionId { get; set; }
    }
}
