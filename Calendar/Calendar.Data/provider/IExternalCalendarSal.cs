using Calendar.Models;
using Calendar.Models.GoogleCalendar;

namespace Calendar.DataAccess.Facade.provider
{
    public interface IExternalCalendarSal
    {
        Task<IEnumerable<CalendarEvent>> FetchEventsAsync();
        Task CreateEventAsync(CalendarEvent calendarEvent);
        Task UpdateEventAsync(CalendarEvent calendarEvent);
        Task DeleteEventAsync(string eventId);
        Task<GoogleTokenResponse> ExchangeCodeForAccessTokenAsync(string code);
        Task<GoogleTokenResponse> RefreshAccessTokenAsync(string refreshToken);
        Task RevokeAccessTokenAsync(string accessToken);
    }
}
