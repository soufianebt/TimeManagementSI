using Calendar.DataAccess.Facade.provider;
using Calendar.Models;
using Calendar.Models.GoogleCalendar;
using Newtonsoft.Json;

namespace Calendar.DataAccess.Imp
{
    public class GoogleCalendarSal : IExternalCalendarSal
    {

        private readonly HttpClient _httpClient;
        private readonly AppSettings _appSettings;
        public GoogleCalendarSal(HttpClient httpClient, AppSettings appSettings)
        {
            _httpClient = httpClient;
            _appSettings = appSettings;
        }
        public async Task<IEnumerable<CalendarEvent>> FetchEventsAsync()
        {
            var calendarEvents = new List<CalendarEvent>();
            return calendarEvents;
        }

        public async Task CreateEventAsync(CalendarEvent calendarEvent)
        {
        }

        public async Task UpdateEventAsync(CalendarEvent calendarEvent)
        {
          
        }

        public async Task DeleteEventAsync(string eventId)
        {
        }

        public async Task<GoogleTokenResponse> ExchangeCodeForAccessTokenAsync(string code)
        {
            var tokenRequest = new Dictionary<string, string>
            {
                { "code", code },
                { "client_id", _appSettings.GoogleApiSettings.ClientId },  
                { "client_secret", _appSettings.GoogleApiSettings.ClientSecret },  
                { "redirect_uri", _appSettings.GoogleApiSettings.RedirectUri },  
                { "grant_type", "authorization_code" }
            };

            var requestContent = new FormUrlEncodedContent(tokenRequest);

            var response = await _httpClient.PostAsync("https://oauth2.googleapis.com/token", requestContent);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var tokenResponse = JsonConvert.DeserializeObject<GoogleTokenResponse>(jsonResponse);
                return tokenResponse;
            }
            return null;
        }

        public async Task<GoogleTokenResponse> RefreshAccessTokenAsync(string refreshToken)
        {
            var refreshTokenRequest = new Dictionary<string, string>
            {
                { "client_id", _appSettings.GoogleApiSettings.ClientId },
                { "client_secret", _appSettings.GoogleApiSettings.ClientSecret },
                { "refresh_token", refreshToken },
                { "grant_type", "refresh_token" }
            };

            var requestContent = new FormUrlEncodedContent(refreshTokenRequest);
            var response = await _httpClient.PostAsync("https://oauth2.googleapis.com/token", requestContent);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var tokenResponse = JsonConvert.DeserializeObject<GoogleTokenResponse>(jsonResponse);
                return tokenResponse;
            }

            return null;
        }

        public async Task RevokeAccessTokenAsync(string accessToken)
        {
            var revokeTokenRequest = new Dictionary<string, string>
            {
                { "token", accessToken }
            };

            var requestContent = new FormUrlEncodedContent(revokeTokenRequest);
            var response = await _httpClient.PostAsync("https://oauth2.googleapis.com/revoke", requestContent);

            if (!response.IsSuccessStatusCode)
            {
                // Handle failure to revoke token if needed
                throw new Exception("Failed to revoke access token");
            }
        }
    }
}
