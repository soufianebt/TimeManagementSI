using Microsoft.AspNetCore.Mvc;
using Calendar.Models;
using Calendar.Service.Facade.services;


namespace dailyManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalendarController : ControllerBase
    {
        private readonly ICalendarService _calendarService;

        public CalendarController(ICalendarService calendarService)
        {
            _calendarService = calendarService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CalendarEvent>>> GetEvents()
        {
            var events = await _calendarService.GetEventsAsync();
            return Ok(events);
        }

        [HttpPost]
        public async Task<IActionResult> AddEvent(CalendarEvent calendarEvent)
        {
            await _calendarService.AddEventAsync(calendarEvent);
            return CreatedAtAction(nameof(GetEvents), new { id = calendarEvent.Id }, calendarEvent);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(string id, CalendarEvent calendarEvent)
        {
            if (id != calendarEvent.Id)
            {
                return BadRequest();
            }

            await _calendarService.UpdateEventAsync(calendarEvent);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(string id)
        {
            await _calendarService.DeleteEventAsync(id);
            return NoContent();
        }
    }
}