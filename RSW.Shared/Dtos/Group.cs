namespace RSW.Shared.Dto
{
    public class GroupDto : BaseEntityDto
    {
        public required string Name { get; set; }
        public Guid AssociationId { get; set; }
    }
}
