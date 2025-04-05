namespace RSW.WebApp.Services
{
    public class TimeZoneService
    {
        private readonly TimeZoneInfo _timeZone;

        public TimeZoneService()
        {
            // Kies de juiste tijdzone (bijv. Amsterdam)
            _timeZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
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
