using RSW.Shared.Dto;

namespace RSW.Shared.Interfaces
{
    public interface IWebSettingService
    {
        Task<IEnumerable<WebSettingDto>> GetAllAsync();
        Task<WebSettingDto?> GetByIdAsync(Guid id);
        Task<WebSettingDto> CreateAsync(WebSettingCreateDto dto);
        Task<WebSettingDto?> UpdateAsync(Guid id, WebSettingDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}