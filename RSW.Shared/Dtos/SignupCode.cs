using System;
using System.ComponentModel.DataAnnotations;

namespace RSW.Shared.Dto
{
    // Voor aanmaken
    public class SignupCodeCreateDto
    {
        [Required]
        public string Code { get; set; } = string.Empty;

        [Required]
        public Guid GroupId { get; set; }

        public DateTime ExpiryDate { get; set; } = DateTime.UtcNow.AddDays(7);

        public bool Lock { get; set; } = false;
    }

    // Voor bijwerken
    public class SignupCodeUpdateDto
    {
        [Required]
        public string Code { get; set; } = string.Empty;

        [Required]
        public Guid GroupId { get; set; }

        public DateTime ExpiryDate { get; set; } = DateTime.UtcNow.AddDays(7);

        public bool Lock { get; set; } = false;
    }

    // Voor terugsturen naar de client
    public class SignupCodeReadDto : BaseEntityDto
    {
        public string Code { get; set; } = string.Empty;

        public Guid GroupId { get; set; }

        public DateTime ExpiryDate { get; set; }

        public bool Lock { get; set; }
    }
}
