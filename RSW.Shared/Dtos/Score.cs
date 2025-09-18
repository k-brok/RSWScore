using RSW.Shared.Entities;

namespace RSW.Shared.Dto
{
    public class ScoreDto : BaseEntityDto
    {
        public Guid PatrolId { get; set; }
        public Guid CriteriaId { get; set; }
        public int Value { get; set; } = 0;
        
    }
    public static class ScoreExtensions
    {
        public static ScoreDto ToDto(this Score score)
        {
            return new ScoreDto
            {
                Id = score.Id,
                PatrolId = score.PatrolId,
                CriteriaId = score.CriteriaId,
                Value = score.Value
            };
        }

        public static Score ToEntity(this ScoreDto score)
        {
            return new Score
            {
                Id = score.Id,
                PatrolId = score.PatrolId,
                CriteriaId = score.CriteriaId,
                Value = score.Value
            };
        }
    }
}
