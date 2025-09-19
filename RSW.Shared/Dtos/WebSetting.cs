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
    public class WebSettingUpdateDto
    {
        [Required]
        public string Key { get; set; } = string.Empty;

        public string Value { get; set; } = string.Empty;

        public string? Description { get; set; } = string.Empty;

        public string? ValueType { get; set; } = string.Empty;

        public string? Category { get; set; } = string.Empty;
    }

    // Voor teruggeven aan de client
    public class WebSettingReadDto : BaseEntityDto
    {
        public string Key { get; set; } = string.Empty;

        public string Value { get; set; } = string.Empty;

        public string? Description { get; set; } = string.Empty;

        public string? ValueType { get; set; } = string.Empty;

        public string? Category { get; set; } = string.Empty;
    }
}
