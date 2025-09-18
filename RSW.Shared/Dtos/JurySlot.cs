using RSW.Shared.Entities;

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
    public static class JurySlotExtensions
    {
        public static JurySlotDto ToDto(this JurySlot juryslot)
        {
            return new JurySlotDto
            {
                Id = juryslot.Id,
                OpeningTime = juryslot.OpeningTime,
                ClosingTime = juryslot.ClosingTime,
                Code = juryslot.Code,
                CategoryId = juryslot.CategoryId,
                SubgroupId = juryslot.SubgroupId,
                EditionId = juryslot.EditionId
            };
        }

        public static JurySlot ToEntity(this JurySlotDto juryslot)
        {
            return new JurySlot
            {
                Id = juryslot.Id,
                OpeningTime = juryslot.OpeningTime,
                ClosingTime = juryslot.ClosingTime,
                Code = juryslot.Code,
                CategoryId = juryslot.CategoryId,
                SubgroupId = juryslot.SubgroupId,
                EditionId = juryslot.EditionId
            };
        }
    }
}
