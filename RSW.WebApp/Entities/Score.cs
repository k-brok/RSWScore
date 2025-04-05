namespace RSW.WebApp.Entities
{
    public class Score : BaseEntity
    {
        public Patrol Patrol { get; set; }
        public int PatrolId { get; set; }
        public int CriteriaId { get; set; }
        public Criteria Criteria { get; set; }
        public int Value { get; set; } = 0;
        
    }
}
