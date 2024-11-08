using EventCheckinSystem.Repo.Data;
using Microsoft.AspNetCore.Mvc;
using EventCheckinSystem.Services.Interfaces;
using EventCheckinSystem.Repo.DTOs;
using EventCheckinSystem.Repo.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using EventCheckinSystem.Repo.DTOs.ResponseDTO;
using EventCheckinSystem.Repo.DTOs.CreateDTO;
using EventCheckinSystem.Repo.DTOs.Paging;
using EventCheckinSystem.Services.Services;

namespace EventCheckinSystem.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class EventController : ControllerBase
    {
        private readonly IEventServices _eventServices;

        public EventController(IEventServices eventServices)
        {
            _eventServices = eventServices;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<EventResponse>>> GetAllEvents([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var pageRequest = new PageRequest { PageNumber = pageNumber, PageSize = pageSize };
                var events = await _eventServices.GetPagedEventsAsync(pageRequest);
                return Ok(events);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("get-all-event-incoming")]
        public async Task<ActionResult<PagedResult<EventResponse>>> GetAllEventsIncoming([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var pageRequest = new PageRequest { PageNumber = pageNumber, PageSize = pageSize };
                var events = await _eventServices.GetPagedIncomingEventsAsync(pageRequest);
                if (events.TotalCount == 0)
                {
                    return NotFound("Không có sự kiện sắp diễn ra");
                }
                return Ok(events);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<EventResponse>> GetEventById(int id)
        {
            var eventDto = await _eventServices.GetEventByIdAsync(id);
            if (eventDto == null)
            {
                return NotFound();
            }
            return Ok(eventDto);
        }
        [HttpGet("get-total-event-by-month")]
        public async Task<ActionResult<EventResponse>> GetTotalEventInMonth(int month, int year)
        {
            var totalEvent = await _eventServices.GetTotalEventByMonth(month, year);
            if (totalEvent == 0)
            {
                return NotFound();
            }
            return Ok(totalEvent);
        }
        [HttpGet("get-event-by-month")]
        public async Task<ActionResult<EventResponse>> GetEventsInMonth([FromQuery]int month, int year, int pageIndex = 1, int pageSize = 10)
        {
            var pageRequest = new PageRequest { PageNumber = pageIndex, PageSize = pageSize };
            var events = await _eventServices.GetEventByMonth(month, year, pageRequest);
            if (events == null)
            {
                return NotFound();
            }
            return Ok(events);
        }


        [HttpPost]
        public async Task<ActionResult<EventResponse>> CreateEvent([FromBody] CreateEventDTO eventDto)
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
