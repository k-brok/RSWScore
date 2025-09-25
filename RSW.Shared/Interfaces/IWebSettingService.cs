using RSW.Shared.Entities;

namespace RSW.Shared.Interfaces
{
    public interface IWebSettingService
    {
        Task<IEnumerable<WebSetting>> GetAllAsync();
        Task<WebSetting?> GetByIdAsync(Guid id);
        Task<WebSetting> CreateAsync(WebSetting model);
        Task<WebSetting?> UpdateAsync(Guid id, WebSetting model);
        Task<bool> DeleteAsync(Guid id);
    }
}