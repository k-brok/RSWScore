using System.ComponentModel.DataAnnotations;

namespace RSW.Shared.Dto
{
    public class AssociationCreateDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        public string Abbreviation { get; set; } = string.Empty;
    }
    public class AssociationDto : BaseEntityDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public string Abbreviation { get; set; } = string.Empty;
    }
}
