namespace RSW.Shared.Dto
{
    public class SubGroupDto : BaseEntityDto
    {
        public required string Color { get; set; }
        public required Guid EditionId { get; set; }
    }
}
