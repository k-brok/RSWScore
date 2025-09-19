using System;
using System.ComponentModel.DataAnnotations;

namespace RSW.Shared.Dto
{
    // Voor aanmaken
    public class SignupCodeCreateDto
    {

        [Required]
        public Guid GroupId { get; set; }
    }

    // Voor bijwerken
    public class SignupCodeUpdateDto
    {

        [Required]
        public Guid GroupId { get; set; }

        public DateTime ExpiryDate { get; set; } = DateTime.UtcNow.AddDays(7);

        public bool Lock { get; set; } = false;
    }

    // Voor terugsturen naar de client
    public class SignupCodeReadDto : BaseEntityDto
    {

        public Guid GroupId { get; set; }

        public DateTime ExpiryDate { get; set; }

        public bool Lock { get; set; }
    }
}
