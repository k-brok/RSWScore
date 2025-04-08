using System.ComponentModel.DataAnnotations.Schema;

namespace RSW.WebApp.Entities
{
    public class Patrol : BaseEntity
    {
        public string Name { get; set; }
        public int? Number { get; set; } = null;
        public int? SubGroupId { get; set; } = null;
        public SubGroup? SubGroup { get; set; } = null;
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public List<Score> Scores { get; set; } = new List<Score>();
        public decimal? TotalScore { get; set; } = null;
        public int? position { get; set; } = null;
        public bool IsDisqualified {
            get {
                DisqualifiedMessages = new List<string>();
                bool disqualified = false;

                DateOnly LSWDate = DateOnly.FromDateTime(DateTime.Now);

                if (this.SubGroup != null)
                {
                    if (this.SubGroup.Edition != null)
                    {
                        if (this.SubGroup.Edition.LSWStartDate != null)
                        {
                            LSWDate = (DateOnly)this.SubGroup.Edition.LSWStartDate;
                        }
                    }
                }

                if (LSWDate == DateOnly.FromDateTime(DateTime.Now))
                {
                    DisqualifiedMessages.Add("Error in LSW Date!");
                    disqualified = true;
                }

                if (this.Scouts.Count <= 4)
                {
                    DisqualifiedMessages.Add("Te wijnig scouts!");
                    disqualified = true;
                }
                    

                if (this.Scouts.Count >= 8)
                {
                    DisqualifiedMessages.Add("Te veel scouts!");
                    disqualified = true;
                }

                if (this.Scouts.Where(S => S.CalculateAge(LSWDate) >= 16).Any())
                {
                    DisqualifiedMessages.Add("Maximale leeftijd op LSW is 15 jaar!");
                    disqualified = true;
                }

                if (this.Scouts.Where(S => S.CalculateAge(LSWDate) <= 10).Any())
                {
                    DisqualifiedMessages.Add("Minimale leeftijd op LSW is 11 jaar!");
                    disqualified = true;
                }

                if (this.Scouts.Count == 5)
                {
                    if (this.Scouts.Where(S => S.CalculateAge(LSWDate) == 15).Count() >= 3)
                    {
                        DisqualifiedMessages.Add("Te veel van 15 jaar!");
                        disqualified = true;
                    }
                }

                if (this.Scouts.Count == 6 || this.Scouts.Count == 7)
                {
                    if (this.Scouts.Where(S => S.CalculateAge(LSWDate) == 15).Count() >= 4)
                    {
                        DisqualifiedMessages.Add("Te veel van 15 jaar!");
                        disqualified = true;
                    }
                }

                return disqualified;
            }
        }
        [NotMapped] public List<string> DisqualifiedMessages { get; set; } = new List<string>();
        public bool IsYoungest { get; set; } = false;
        public List<Scout> Scouts { get; set; } = new List<Scout>();
        [NotMapped] public string StringNumber { get { return this.Number.ToString(); } }
        
    }
}
