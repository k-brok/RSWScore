using System;
using System.ComponentModel.DataAnnotations;

namespace RSW.Shared.Dto
{
    // Voor aanmaken
    public class WebSettingCreateDto
    {
        [Required]
        public string Key { get; set; } = string.Empty;

        public string Value { get; set; } = string.Empty;

        public string? Description { get; set; } = string.Empty;

        public string? ValueType { get; set; } = string.Empty;

        public string? Category { get; set; } = string.Empty;
    }

    // Voor bijwerken
    public class WebSettingDto : BaseEntityDto
    {
        [Required]
        public string Key { get; set; } = string.Empty;

        public string Value { get; set; } = string.Empty;

        public string? Description { get; set; } = string.Empty;

        public string? ValueType { get; set; } = string.Empty;

        public string? Category { get; set; } = string.Empty;
    }
}
