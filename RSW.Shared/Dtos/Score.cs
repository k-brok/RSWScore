namespace RSW.Shared.Dto
{
    public class ScoreDto : BaseEntityDto
    {
        public Guid PatrolId { get; set; }
        public Guid CriteriaId { get; set; }
        public int Value { get; set; } = 0;
        
    }
}
