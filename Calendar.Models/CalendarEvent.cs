namespace Calendar.Models
{
    public class CalendarEvent
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
    }

    public class GoogleCalendarEventsResponse
    {
        public List<GoogleEvent> Items { get; set; }
    }

    public class GoogleEvent
    {
        public string Id { get; set; }
        public string Summary { get; set; }
        public GoogleEventDateTime Start { get; set; }
        public GoogleEventDateTime End { get; set; }
        public string Description { get; set; }
    }

    public class GoogleEventDateTime
    {
        public DateTime? DateTime { get; set; }
        public string TimeZone { get; set; } 
    }


}
