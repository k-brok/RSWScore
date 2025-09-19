using RSW.Shared.Dto;

namespace RSW.Shared.Interfaces
{
    public interface IWebSettingService
    {
        Task<IEnumerable<WebSettingReadDto>> GetAllAsync();
        Task<WebSettingReadDto?> GetByIdAsync(Guid id);
        Task<WebSettingReadDto> CreateAsync(WebSettingCreateDto dto);
        Task<WebSettingReadDto?> UpdateAsync(Guid id, WebSettingUpdateDto dto);
        Task<bool> DeleteAsync(Guid id);
    }
}