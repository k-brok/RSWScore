using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using RSW.Shared.Entities;

namespace RSW.Shared.Dto
{
    public class EditionCreateDto
    {
        [Required]
        public DateOnly RSWStartDate { get; set; }
        public DateOnly? LSWStartDate { get; set; }
        public string? Theme { get; set; }
    }
    public class EditionDto : BaseEntityDto
    {
        [Required]
        public DateOnly RSWStartDate { get; set; }
        public DateOnly? LSWStartDate { get; set; } = null;
        public DateOnly? PreSignupClose { get; set; } = null;
        public string? Theme { get; set; }
        public bool IsActive { get; set; }
    }
}
