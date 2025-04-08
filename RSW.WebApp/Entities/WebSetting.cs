using System.ComponentModel.DataAnnotations;

namespace RSW.WebApp.Entities
{
    public class WebSetting : BaseEntity
    {
        [Required]
        public string Key { get; set; }
        public string Value { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public string? ValueType { get; set; } = string.Empty;
        public string? Category { get; set; } = string.Empty;
    }
}
