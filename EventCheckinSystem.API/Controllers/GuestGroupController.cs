using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Repo.Repositories.Interfaces;
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
        private readonly IGuestGroupRepo _guestGroupRepo;

        public GuestGroupController(IGuestGroupRepo guestGroupRepo)
        {
            _guestGroupRepo = guestGroupRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GuestGroup>>> GetAllGuestGroups()
        {
            var groups = await _guestGroupRepo.GetAllGuestGroupsAsync();
            return Ok(groups);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GuestGroup>> GetGuestGroupById(int id)
        {
            try
            {
                var group = await _guestGroupRepo.GetGuestGroupByIdAsync(id);
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
        public async Task<ActionResult<GuestGroup>> CreateGuestGroup([FromBody] GuestGroup newGroup)
        {
            var createdGroup = await _guestGroupRepo.CreateGuestGroupAsync(newGroup);
            return CreatedAtAction(nameof(GetGuestGroupById), new { id = createdGroup.GuestGroupID }, createdGroup);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGuestGroup([FromBody] GuestGroup updatedGroup, [FromQuery] string updatedBy)
        {
            try
            {
                await _guestGroupRepo.UpdateGuestGroupAsync(updatedGroup, updatedBy);
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
                await _guestGroupRepo.DeleteGuestGroupAsync(id);
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
        public async Task<ActionResult<GuestGroup>> GetGuestGroupByGuestId(int guestId)
        {
            try
            {
                var group = await _guestGroupRepo.GetGuestGroupByGuestIdAsync(guestId);
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
