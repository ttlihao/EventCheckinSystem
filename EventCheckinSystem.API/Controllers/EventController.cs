using EventCheckinSystem.Repo.Data;
using Microsoft.AspNetCore.Mvc;
using EventCheckinSystem.Services.Interfaces;
using EventCheckinSystem.Repo.DTOs;
using EventCheckinSystem.Repo.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace EventCheckinSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventServices _eventServices;

        public EventController(IEventServices eventServices)
        {
            _eventServices = eventServices;
        }

        [HttpGet]
        [Authorize(Roles ="admin")]
        public async Task<ActionResult<IEnumerable<EventDTO>>> GetAllEvents()
        {
            var events = await _eventServices.GetAllEventsAsync();
            return Ok(events);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EventDTO>> GetEventById(int id)
        {
            var eventDto = await _eventServices.GetEventByIdAsync(id);
            if (eventDto == null)
            {
                return NotFound();
            }
            return Ok(eventDto);
        }

        [HttpPost]
        public async Task<ActionResult<EventDTO>> CreateEvent([FromBody] CreateEventDTO eventDto)
        {
            var createdEvent = await _eventServices.CreateEventAsync(eventDto);
            return CreatedAtAction(nameof(GetEventById), new { id = createdEvent.EventID }, createdEvent);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateEvent([FromBody] EventDTO eventDto)
        {
           var result = await _eventServices.UpdateEventAsync(eventDto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            var result = await _eventServices.DeleteEventAsync(id);
            return Ok(result);
        }
    }
}
