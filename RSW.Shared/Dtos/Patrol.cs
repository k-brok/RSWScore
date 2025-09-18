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
}
