namespace RSW.Shared.Dto
{
    public class SubGroupDto : BaseEntityDto
    {
        public required string Color { get; set; }
        public required Guid EditionId { get; set; }
    }
    public static class SubGroupExtensions
    {
        public static SubGroupDto ToDto(this Entities.SubGroup subgroup)
        {
            return new SubGroupDto
            {
                Id = subgroup.Id,
                Color = subgroup.Color,
                EditionId = subgroup.EditionId
            };
        }
        public static Entities.SubGroup ToEntity(this SubGroupDto subgroup)
        {
            return new Entities.SubGroup
            {
                Id = subgroup.Id,
                Color = subgroup.Color,
                EditionId = subgroup.EditionId
            };
        }
    }
}
