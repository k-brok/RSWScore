using System.ComponentModel.DataAnnotations;

namespace RSW.Shared.Dto
{
    public class GroupCreateDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public Guid AssociationId { get; set; }
    }

    public class GroupUpdateDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public Guid AssociationId { get; set; }
    }

    public class GroupReadDto : BaseEntityDto
    {
        public string Name { get; set; } = string.Empty;

        public Guid AssociationId { get; set; }
    }
}
