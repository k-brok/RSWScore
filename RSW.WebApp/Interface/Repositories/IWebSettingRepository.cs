using RSW.WebApp.Entities;

namespace RSW.WebApp.Interface.Repositories
{
    public interface IWebSettingRepository
    {
        Task<List<WebSetting>> GetAllAsync();
        Task<WebSetting> GetByKeyAsync(string name);
        Task Save(WebSetting websetting);
        Task RevertEdits(WebSetting websetting);
        Task Delete(WebSetting websetting);
    }
}
