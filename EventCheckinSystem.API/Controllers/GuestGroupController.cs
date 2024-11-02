using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Repo.DTOs;
using EventCheckinSystem.Repo.DTOs.CreateDTO;
using EventCheckinSystem.Repo.DTOs.ResponseDTO;
using EventCheckinSystem.Repo.Repositories.Interfaces;
using EventCheckinSystem.Services.Interfaces;
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
        public async Task<ActionResult<IEnumerable<GuestGroupResponse>>> GetAllGuestGroups()
        {
            var groups = await _guestGroupService.GetAllGuestGroupsAsync();
            return Ok(groups);
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
    }
}
