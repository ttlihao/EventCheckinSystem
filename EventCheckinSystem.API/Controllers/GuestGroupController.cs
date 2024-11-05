using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Repo.DTOs;
using EventCheckinSystem.Repo.DTOs.CreateDTO;
using EventCheckinSystem.Repo.DTOs.Paging;
using EventCheckinSystem.Repo.DTOs.ResponseDTO;
using EventCheckinSystem.Repo.Repositories.Interfaces;
using EventCheckinSystem.Services.Interfaces;
using EventCheckinSystem.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCheckinSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GuestGroupController : ControllerBase
    {
        private readonly IGuestGroupServices _guestGroupService;

        public GuestGroupController(IGuestGroupServices guestGroupService)
        {
            _guestGroupService = guestGroupService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<GuestGroupResponse>>> GetPagedGuestGroups([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var pageRequest = new PageRequest { PageNumber = pageNumber, PageSize = pageSize };
                var guestGroups = await _guestGroupService.GetPagedGuestGroupsAsync(pageRequest);
                return Ok(guestGroups);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GuestGroupResponse>> GetGuestGroupById(int id)
        {
            try
            {
                var group = await _guestGroupService.GetGuestGroupByIdAsync(id);
                return Ok(group);
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<GuestGroupDTO>> CreateGuestGroup([FromBody] CreateGuestGroupDTO newGroup)
        {
            var createdGroup = await _guestGroupService.CreateGuestGroupAsync(newGroup);
            return CreatedAtAction(nameof(GetGuestGroupById), new { id = createdGroup.GuestGroupID }, createdGroup);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGuestGroup([FromBody] GuestGroupDTO updatedGroup)
        {
            try
            {
                await _guestGroupService.UpdateGuestGroupAsync(updatedGroup);
                return Ok();
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGuestGroup(int id)
        {
            try
            {
                await _guestGroupService.DeleteGuestGroupAsync(id);
                return Ok();
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("byguest/{guestId}")]
        public async Task<ActionResult<GuestGroupResponse>> GetGuestGroupByGuestId(int guestId)
        {
            try
            {
                var group = await _guestGroupService.GetGuestGroupByGuestIdAsync(guestId);
                return Ok(group);
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("user/{userId}/guest-groups")]
        public async Task<ActionResult<List<GuestGroupDTO>>> GetGuestGroupsByUserId(string userId)
        {
            try
            {
                var guestGroups = await _guestGroupService.GetGuestGroupsByUserIdAsync(userId);
                if (guestGroups == null || guestGroups.Count == 0)
                {
                    return NotFound($"No guest groups found for user ID: {userId}");
                }
                return Ok(guestGroups);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("event/{eventId}/guest-groups")]
        public async Task<ActionResult<List<GuestGroupDTO>>> GetGuestGroupsByEventId(int eventId)
        {
            try
            {
                var guestGroups = await _guestGroupService.GetGuestGroupsByEventIdAsync(eventId);
                if (guestGroups == null || guestGroups.Count == 0)
                {
                    return NotFound($"No guest groups found for event ID: {eventId}");
                }
                return Ok(guestGroups);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

    }
}
