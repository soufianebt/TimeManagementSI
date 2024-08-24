using Newtonsoft.Json;
using System;

namespace Calendar.Models.GoogleCalendar
{
    public class GoogleTokenResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty("scope")]
        public string Scope { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        public DateTime TokenIssued { get; set; } = DateTime.UtcNow; // When the token was issued

        public DateTime Expiry => TokenIssued.AddSeconds(ExpiresIn); // Token Expiry Time

        // Use GUID as the primary key
        public Guid Id { get; set; } = Guid.NewGuid(); // Automatically generate a GUID when a new token is created
    }
}
