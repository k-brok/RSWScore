namespace RSW.Shared.Dto
{
    public class JurySlotDto : BaseEntityDto
    {
        public DateTime OpeningTime { get; set; } = DateTime.UtcNow;
        public DateTime ClosingTime { get; set; } = DateTime.UtcNow.AddHours(2);
        public required string Code { get; set; }
        public int CategoryId { get; set; }
        public int SubgroupId { get; set; }
        public int EditionId { get; set; }
    }
}
