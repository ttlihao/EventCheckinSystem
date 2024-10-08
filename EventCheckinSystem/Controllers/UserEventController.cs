using EventCheckinSystem.Services.Interfaces;
using EventCheckinSystem.Repo.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCheckinSystem.API.Controllers
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

        // GET: api/UserEvent
        [HttpGet]
        public async Task<IActionResult> GetAllUserEvents()
        {
            var userEvents = await _userEventServices.GetAllUserEventsAsync();
            return Ok(userEvents);
        }

        // GET: api/UserEvent/{userId}/{eventId}
        [HttpGet("{userId}/{eventId}")]
        public async Task<IActionResult> GetUserEvent(string userId, int eventId)
        {
            var userEvent = await _userEventServices.GetUserEventAsync(userId, eventId);
            if (userEvent == null)
            {
                return NotFound($"No relationship found for User ID {userId} and Event ID {eventId}");
            }

            return Ok(userEvent);
        }

        // POST: api/UserEvent
        [HttpPost]
        public async Task<IActionResult> CreateUserEvent([FromBody] UserEvent userEvent)
        {
            if (userEvent == null)
            {
                return BadRequest("UserEvent data is null.");
            }

            var createdUserEvent = await _userEventServices.CreateUserEventAsync(userEvent);
            return CreatedAtAction(nameof(GetUserEvent), new { userId = createdUserEvent.UserID, eventId = createdUserEvent.EventID }, createdUserEvent);
        }

        // DELETE: api/UserEvent/{userId}/{eventId}
        [HttpDelete("{userId}/{eventId}")]
        public async Task<IActionResult> DeleteUserEvent(string userId, int eventId)
        {
            await _userEventServices.DeleteUserEventAsync(userId, eventId);
            return NoContent();
        }
    }
}
