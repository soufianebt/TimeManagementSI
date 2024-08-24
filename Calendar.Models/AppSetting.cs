namespace Calendar.Models
{
    public class AppSettings
    {
        public LoggingSettings Logging { get; set; }
        public GoogleApiSettings GoogleApiSettings { get; set; }
        public string AllowedHosts { get; set; }
    }

    public class LoggingSettings
    {
        public LogLevelSettings LogLevel { get; set; }
    }

    public class LogLevelSettings
    {
        public string Default { get; set; }
        public string MicrosoftAspNetCore { get; set; }
    }

    public class GoogleApiSettings
    {
        public string ClientId { get; set; }
        public string ProjectId { get; set; }
        public string ClientSecret { get; set; }
        public string RefreshToken { get; set; }
        public string RedirectUri { get; set; }
        public string AuthUri { get; set; }
        public string TokenUri { get; set; }
    }


}
