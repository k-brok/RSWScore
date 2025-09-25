using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RSW.Shared.Dto
{
    public class SubGroupCreateDto
    {
        [Required]
        public string Color { get; set; } = string.Empty;

        [Required]
        public Guid EditionId { get; set; }
    }

    public class SubGroupDto : BaseEntityDto
    {
        [Required]
        public string Color { get; set; } = string.Empty;

        [Required]
        public Guid EditionId { get; set; }
        [JsonIgnore]
        public IEnumerable<PatrolDto>? Patrols { get; set; }
    }
}
