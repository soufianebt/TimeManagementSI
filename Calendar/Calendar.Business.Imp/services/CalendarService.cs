using Calendar.Models;
using Calendar.Service.Facade.services;
using Calendar.DataAccess.Facade.provider;
using Calendar.Models.GoogleCalendar;

namespace Calendar.Service.Imp.services
{
    public class CalendarService : ICalendarService
    {
        private readonly IExternalCalendarSal _externalCalendarService;
        private readonly AppSettings _appSettings;

        public CalendarService(IExternalCalendarSal externalCalendarService, AppSettings appSettings)
        {
            _externalCalendarService = externalCalendarService;
            _appSettings = appSettings;
        }

        public async Task<IEnumerable<CalendarEvent>> GetEventsAsync()
        {
            return await _externalCalendarService.FetchEventsAsync();
        }

        public async Task AddEventAsync(CalendarEvent calendarEvent)
        {
            await _externalCalendarService.CreateEventAsync(calendarEvent);
        }

        public async Task UpdateEventAsync(CalendarEvent calendarEvent)
        {
            await _externalCalendarService.UpdateEventAsync(calendarEvent);
        }

        public async Task DeleteEventAsync(string eventId)
        {
            await _externalCalendarService.DeleteEventAsync(eventId);
        }

        public async Task<GoogleTokenResponse> ExchangeCodeForAccessTokenAsync(string code)
        {
            return await _externalCalendarService.ExchangeCodeForAccessTokenAsync(code);
        }
        public async Task<GoogleTokenResponse> RefreshAccessTokenAsync(string refreshToken)
        {
            return await _externalCalendarService.RefreshAccessTokenAsync(refreshToken);
        }

        public async Task RevokeAccessTokenAsync(string accessToken)
        {
            // Assuming _externalCalendarService handles the revocation logic
            await _externalCalendarService.RevokeAccessTokenAsync(accessToken);
        }

        public Task<string> GetRedirectionUrl()
        {
            return Task.FromResult($"https://accounts.google.com/o/oauth2/v2/auth?" +
                        $"scope=https://www.googleapis.com/auth/calendar+https://www.googleapis.com/auth/calendar.events+https://www.googleapis.com/auth/tasks&" +
                        $"access_type=offline&" +
                        $"include_granted_scopes=true&" +
                        $"response_type=code&" +
                        $"state=there&" +
                        $"redirect_uri={_appSettings.GoogleApiSettings.RedirectUri}&" +
                        $"client_id={_appSettings.GoogleApiSettings.ClientId}");

        }
    }
}
