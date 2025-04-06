using RSW.WebApp.Entities;
using RSW.WebApp.Interface.Repositories;

namespace RSW.WebApp.Services
{
    public class SettingsService
    {
        public List<WebSetting> Settings { get; set; } = new List<WebSetting>();
        public string GetString(string key)
        {
            WebSetting setting = Settings.FirstOrDefault(s => s.Key == key);
            if (setting != null)
            {
                return setting.Value;
            }
            return string.Empty;
        }
    }
}
