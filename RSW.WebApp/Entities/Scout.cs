using System.ComponentModel.DataAnnotations.Schema;

namespace RSW.WebApp.Entities
{
    public class Scout : BaseEntity
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public bool IsPL { get; set; } = false;
        public bool IsAPL { get; set; } = false;
        public Patrol Patrol { get; set; }
        public int PatrolId { get; set; }
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
        public bool SetAPL(Patrol patrol)
        {
            if (this.IsPL)
                return false;
            foreach (Scout scout in patrol.Scouts.Where(S => S.IsAPL))
                scout.IsAPL = false;

            this.IsAPL = true;

            return true;
        }
        public bool SetPL(Patrol patrol)
        {
            if (this.IsAPL)
                return false;

            foreach (Scout scout in patrol.Scouts.Where(S => S.IsPL))
                scout.IsPL = false;

            this.IsPL = true;

            return true;
        }
    }
}
