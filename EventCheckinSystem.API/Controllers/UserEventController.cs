using EventCheckinSystem.Repo.DTOs;
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
        public async Task<ActionResult<IEnumerable<UserEventDTO>>> GetAllUserEvents()
        {
            try
            {
                var userEvents = await _userEventService.GetAllUserEventsAsync();
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
    }
}
