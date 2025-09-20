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

    public class GroupDto : BaseEntityDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public Guid AssociationId { get; set; }
    }
}
