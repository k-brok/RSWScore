using System.ComponentModel.DataAnnotations.Schema;

namespace RSW.Shared.Entities
{
    public class Scout : BaseEntity
    {
        public required string Firstname { get; set; }
        public required string Lastname { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public bool IsPL { get; set; } = false;
        public bool IsAPL { get; set; } = false;
        public Patrol Patrol { get; set; } = null!;
        public required Guid PatrolId { get; set; }
        [NotMapped]
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
