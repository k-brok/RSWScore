using RSW.Shared.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace RSW.Shared.Dto
{
    public class PatrolDto : BaseEntityDto
    {
        public required string Name { get; set; }
        public int? Number { get; set; } = null;
        public Guid SubGroupId { get; set; }
        public int GroupId { get; set; }
        public decimal? TotalScore { get; set; } = null;
        public int? position { get; set; } = null;
        public bool IsYoungest { get; set; } = false;
        
    }
    public static class PatrolExtensions
    {
        public static PatrolDto ToDto(this Patrol patrol)
        {
            return new PatrolDto
            {
                Id = patrol.Id,
                Name = patrol.Name,
                Number = patrol.Number,
                SubGroupId = patrol.SubGroupId,
                GroupId = patrol.GroupId,
                TotalScore = patrol.TotalScore,
                position = patrol.position,
                IsYoungest = patrol.IsYoungest
            };
        }

        public static Patrol ToEntity(this PatrolDto patrol)
        {
            return new Patrol
            {
                Id = patrol.Id,
                Name = patrol.Name,
                Number = patrol.Number,
                SubGroupId = patrol.SubGroupId,
                GroupId = patrol.GroupId,
                TotalScore = patrol.TotalScore,
                position = patrol.position,
                IsYoungest = patrol.IsYoungest
            };
        }
    }
}
