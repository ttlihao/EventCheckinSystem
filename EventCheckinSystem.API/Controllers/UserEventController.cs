using EventCheckinSystem.Repo.DTOs;
using EventCheckinSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCheckinSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserEventController : ControllerBase
    {
        private readonly IUserEventServices _userEventServices;

        public UserEventController(IUserEventServices userEventServices)
        {
            _userEventServices = userEventServices;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserEventDTO>>> GetAllUserEvents()
        {
            var userEvents = await _userEventServices.GetAllUserEventsAsync();
            return Ok(userEvents);
        }

        [HttpGet("{eventId}")]
        public async Task<ActionResult<UserEventDTO>> GetUserEventById(int eventId)
        {
            var userEvent = await _userEventServices.GetUserEventByIdAsync(eventId);
            if (userEvent == null)
            {
                return NotFound();
            }
            return Ok(userEvent);
        }

        [HttpPost]
        public async Task<ActionResult<UserEventDTO>> AddUserEvent([FromBody] UserEventDTO userEventDto)
        {
            await _userEventServices.AddUserEventAsync(userEventDto);
            return CreatedAtAction(nameof(GetUserEventById), new { eventId = userEventDto.EventID }, userEventDto);
        }

        [HttpPut("{eventId}")]
        public async Task<IActionResult> UpdateUserEvent(int eventId, [FromBody] UserEventDTO userEventDto)
        {
            if (eventId != userEventDto.EventID)
            {
                return BadRequest();
            }

            await _userEventServices.UpdateUserEventAsync(userEventDto);
            return NoContent();
        }

        [HttpDelete("{eventId}")]
        public async Task<IActionResult> DeleteUserEvent(int eventId)
        {
            await _userEventServices.DeleteUserEventAsync(eventId);
            return NoContent();
        }
    }
}
