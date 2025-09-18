using System.ComponentModel.DataAnnotations;

namespace RSW.Shared.Dto
{
    public class WebSettingDto : BaseEntityDto
    {
        [Required]
        public required string Key { get; set; }
        public string Value { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public string? ValueType { get; set; } = string.Empty;
        public string? Category { get; set; } = string.Empty;
    }
    public static class WebSettingExtensions
    {
        public static WebSettingDto ToDto(this Entities.WebSetting webSetting)
        {
            return new WebSettingDto
            {
                Id = webSetting.Id,
                Key = webSetting.Key,
                Value = webSetting.Value,
                Description = webSetting.Description,
                ValueType = webSetting.ValueType,
                Category = webSetting.Category
            };
        }
        public static Entities.WebSetting ToEntity(this WebSettingDto webSetting)
        {
            return new Entities.WebSetting
            {
                Id = webSetting.Id,
                Key = webSetting.Key,
                Value = webSetting.Value,
                Description = webSetting.Description,
                ValueType = webSetting.ValueType,
                Category = webSetting.Category
            };
        }
    }
}
