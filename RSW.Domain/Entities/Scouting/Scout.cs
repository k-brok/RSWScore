using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace RSW.Domain.Entities
{
    public class Scout : BaseEntity
    {
        [Required]
        public string? Firstname { get; set; }
        [Required]
        public string? Lastname { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public bool IsPL { get; set; } = false;
        public bool IsAPL { get; set; } = false;
        [JsonIgnore]
        public Patrol Patrol { get; set; } = null!;
        [Required]
        public Guid PatrolId { get; set; }
        public int Age
        {
            get
            {
                DateOnly today = DateOnly.FromDateTime(DateTime.Today);

                int age = today.Year - DateOfBirth.Year;

                // Corrigeren als de verjaardag dit jaar nog niet is geweest
                if (today < new DateOnly(today.Year, DateOfBirth.Month, DateOfBirth.Day))
                {
                    age--;
                }

                return age;
            }
        }
        public int CalculateAge(DateOnly Date)
        {

            int age = Date.Year - DateOfBirth.Year;

            // Corrigeren als de verjaardag dit jaar nog niet is geweest
            if (Date < new DateOnly(Date.Year, DateOfBirth.Month, DateOfBirth.Day))
            {
                age--;
            }

            return age;
        }
    }
}
