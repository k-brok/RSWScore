using System.ComponentModel.DataAnnotations;

namespace RSW.Shared.Dto
{
    public class AssociationCreateDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public string Abbreviation { get; set; } = string.Empty;
    }
    public class AssociationUpdateDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public string Abbreviation { get; set; } = string.Empty;
    }
    public class AssociationReadDto : BaseEntityDto
    {
        public string Name { get; set; } = string.Empty;
        public string Abbreviation { get; set; } = string.Empty;
    }
}
