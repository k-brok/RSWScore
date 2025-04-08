using RSW.WebApp.Interface.Repositories;
using RSW.WebApp.Repositories;

namespace RSW.WebApp.Services
{
    public class TimeZoneService
    {
        private TimeZoneInfo _timeZone;
        private readonly SettingsService _webSetting;
        public TimeZoneService(SettingsService webSetting)
        {
            _webSetting = webSetting;
            FindTimezone();
        }
        private async Task FindTimezone()
        {
            string timeZoneId = _webSetting.GetString("Timezone");
            _timeZone = TimeZoneInfo.FindSystemTimeZoneById(timeZoneId);
        }
        public DateTime ConvertUtcToLocal(DateTime utcDate)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(utcDate, _timeZone);
        }

        public DateTime ConvertLocalToUtc(DateTime localDate)
        {
            return TimeZoneInfo.ConvertTimeToUtc(localDate, _timeZone);
        }
    }
}
