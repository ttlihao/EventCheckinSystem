using EventCheckinSystem.Repo.DTOs;
using EventCheckinSystem.Repo.DTOs.Paging;
using EventCheckinSystem.Repo.Repositories.Interfaces;
using EventCheckinSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCheckinSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserEventController : ControllerBase
    {
        private readonly IUserEventServices _userEventService;

        public UserEventController(IUserEventServices userEventService)
        {
            _userEventService = userEventService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<UserEventDTO>>> GetPagedUserEvents([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var pageRequest = new PageRequest { PageNumber = pageNumber, PageSize = pageSize };
                var userEvents = await _userEventService.GetPagedUserEventsAsync(pageRequest);
                return Ok(userEvents);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserEventDTO>> GetUserEventById(int id)
        {
            try
            {
                var userEvent = await _userEventService.GetUserEventByIdAsync(id);
                if (userEvent == null)
                {
                    return NotFound($"UserEvent with ID {id} not found.");
                }
                return Ok(userEvent);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddUserEvent([FromBody] UserEventDTO newUserEvent)
        {
            try
            {
                await _userEventService.AddUserEventAsync(newUserEvent);
                return CreatedAtAction(nameof(GetUserEventById), new { id = newUserEvent.EventID }, newUserEvent);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUserEvent([FromBody] UserEventDTO updatedUserEvent)
        {
            try
            {
                await _userEventService.UpdateUserEventAsync(updatedUserEvent);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserEvent(int id)
        {
            try
            {
                await _userEventService.DeleteUserEventAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("user/{userId}/events")]
        public async Task<ActionResult<List<EventDTO>>> GetEventsByUserId(string userId)
        {
            try
            {
                var events = await _userEventService.GetEventsByUserIdAsync(userId);
                if (events == null || events.Count == 0)
                {
                    return NotFound($"No events found for user ID: {userId}");
                }
                return Ok(events);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
