using RSW.Shared.Entities;

namespace RSW.Shared.Dto
{
    public class AssociationDto : BaseEntityDto
    {
        public required string Name { get; set; }
        public string Abbreviation { get; set; } = string.Empty;
    }
    public static class AssociationExtensions
    {
        public static AssociationDto ToDto(this Association association)
        {
            return new AssociationDto
            {
                Id = association.Id,
                Name = association.Name,
                Abbreviation = association.Abbreviation
            };
        }

        public static Association ToEntity(this AssociationDto association)
        {
            return new Association
            {
                Id = association.Id,
                Name = association.Name,
                Abbreviation = association.Abbreviation
            };
        }
    }
}
