namespace RSW.Shared.Dto
{
    public class AssociationDto : BaseEntityDto
    {
        public required string Name { get; set; }
        public string Abbreviation { get; set; } = string.Empty;
    }
}
