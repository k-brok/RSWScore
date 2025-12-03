using RSW.Domain.Entities;

namespace RSW.Application.Interfaces
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