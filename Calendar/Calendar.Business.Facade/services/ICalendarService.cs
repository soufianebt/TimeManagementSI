using Calendar.Models;
using Calendar.Models.GoogleCalendar;

namespace Calendar.Service.Facade.services
{
    public interface ICalendarService
    {
        Task<IEnumerable<CalendarEvent>> GetEventsAsync();
        Task AddEventAsync(CalendarEvent calendarEvent);
        Task UpdateEventAsync(CalendarEvent calendarEvent);
        Task DeleteEventAsync(string eventId);
        Task<GoogleTokenResponse> ExchangeCodeForAccessTokenAsync(string code);
        Task<string> GetRedirectionUrl();
        Task<GoogleTokenResponse> RefreshAccessTokenAsync(string refreshToken);
        Task RevokeAccessTokenAsync(string accessToken);


    }
}
