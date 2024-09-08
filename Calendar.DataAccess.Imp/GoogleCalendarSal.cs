using Calendar.DataAccess.Facade.provider;
using Calendar.Models;
using Calendar.Models.GoogleCalendar;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Calendar.DataAccess.Imp
{
    public class GoogleCalendarSal : IExternalCalendarSal
    {

        private readonly HttpClient _httpClient;
        private readonly AppSettings _appSettings;
        private readonly IGoogleTokenDal _googleTokenDal;
        public GoogleCalendarSal(HttpClient httpClient, AppSettings appSettings, IGoogleTokenDal googleTokenDal)
        {
            _httpClient = httpClient;
            _appSettings = appSettings;
            _googleTokenDal = googleTokenDal;
        }
        public async Task<IEnumerable<CalendarEvent>> FetchEventsAsync()
        {
            await updateClientToken();
            var requestUri = "https://www.googleapis.com/calendar/v3/calendars/primary/events";
            var response = await _httpClient.GetAsync(requestUri);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Failed to fetch events from Google Calendar.");
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var googleEventsResponse = JsonConvert.DeserializeObject<GoogleCalendarEventsResponse>(jsonResponse);

            var calendarEvents = googleEventsResponse.Items.Select(googleEvent => new CalendarEvent
            {
                Id = googleEvent.Id,
                Title = googleEvent.Summary,
                EndDate = googleEvent.End?.DateTime ?? null,
                StartDate = googleEvent.Start?.DateTime ?? null,
                Description = googleEvent.Description
            }).ToList();

            return calendarEvents;
        }


        private async Task updateClientToken()
        {
            GoogleTokenResponse googleToken = await _googleTokenDal.GetFirstToken();

            if (googleToken == null)
            {
                throw new UnauthorizedAccessException("No Google token found. Please authenticate the user.");
            }
            if (googleToken.Expiry <= DateTime.UtcNow)
            {
                var refreshGoogleToken = await RefreshAccessTokenAsync(googleToken.RefreshToken);
                googleToken.AccessToken = refreshGoogleToken.AccessToken;
                googleToken.ExpiresIn = refreshGoogleToken.ExpiresIn;
                googleToken.TokenIssued = refreshGoogleToken.TokenIssued;
                await _googleTokenDal.UpdateTokenAsync(googleToken);
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", googleToken.AccessToken);
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
                await _googleTokenDal.AddTokenAsync(tokenResponse);
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
