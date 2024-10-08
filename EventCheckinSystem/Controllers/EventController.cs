using EventCheckinSystem.Repo.Data;
using Microsoft.AspNetCore.Mvc;
using EventCheckinSystem.Services.Interfaces;

namespace EventCheckinSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventServices _eventService;

        public EventController(IEventServices eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Event>>> GetAllEvents()
        {
            var events = await _eventService.GetAllEventsAsync();
            return Ok(events);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Event>> GetEvent(int id)
        {
            var eventItem = await _eventService.GetEventByIdAsync(id);
            if (eventItem == null)
            {
                return NotFound();
            }
            return Ok(eventItem);
        }

        [HttpPost]
        public async Task<ActionResult<Event>> CreateEvent([FromBody] Event newEvent)
        {
            if (newEvent == null)
            {
                return BadRequest();
            }

            var createdEvent = await _eventService.CreateEventAsync(newEvent);
            return CreatedAtAction(nameof(GetEvent), new { id = createdEvent.EventID }, createdEvent);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEvent(int id, [FromBody] Event updatedEvent)
        {
            if (updatedEvent == null || updatedEvent.EventID != id)
            {
                return BadRequest();
            }

            var existingEvent = await _eventService.GetEventByIdAsync(id);
            if (existingEvent == null)
            {
                return NotFound();
            }

            await _eventService.UpdateEventAsync(updatedEvent);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var eventToDelete = await _eventService.GetEventByIdAsync(id);
            if (eventToDelete == null)
            {
                return NotFound();
            }

            await _eventService.DeleteEventAsync(id);
            return NoContent();
        }
    }
}
