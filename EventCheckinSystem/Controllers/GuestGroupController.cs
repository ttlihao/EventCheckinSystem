using EventCheckinSystem.Repo.DTOs;
using EventCheckinSystem.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCheckinSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestGroupController : ControllerBase
    {
        private readonly IGuestGroupServices _guestGroupServices;

        public GuestGroupController(IGuestGroupServices guestGroupServices)
        {
            _guestGroupServices = guestGroupServices;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GuestGroupDTO>>> GetAllGuestGroups()
        {
            var guestGroups = await _guestGroupServices.GetAllGuestGroupsAsync();
            return Ok(guestGroups);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GuestGroupDTO>> GetGuestGroupById(int id)
        {
            var guestGroup = await _guestGroupServices.GetGuestGroupByIdAsync(id);
            if (guestGroup == null)
            {
                return NotFound();
            }
            return Ok(guestGroup);
        }

        [HttpPost]
        public async Task<ActionResult<GuestGroupDTO>> CreateGuestGroup([FromBody] GuestGroupDTO guestGroupDto)
        {
            if (guestGroupDto == null)
            {
                return BadRequest("GuestGroup data is required.");
            }

            var createdGuestGroup = await _guestGroupServices.CreateGuestGroupAsync(guestGroupDto, User.Identity.Name);
            return CreatedAtAction(nameof(GetGuestGroupById), new { id = createdGuestGroup.GuestGroupID }, createdGuestGroup);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGuestGroup(int id, [FromBody] GuestGroupDTO guestGroupDto)
        {
            if (id != guestGroupDto.GuestGroupID)
            {
                return BadRequest("ID mismatch.");
            }

            await _guestGroupServices.UpdateGuestGroupAsync(guestGroupDto, User.Identity.Name);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGuestGroup(int id)
        {
            await _guestGroupServices.DeleteGuestGroupAsync(id);
            return NoContent();
        }

        [HttpGet("guest/{guestId}")]
        public async Task<ActionResult<GuestGroupDTO>> GetGuestGroupByGuestId(int guestId)
        {
            var guestGroup = await _guestGroupServices.GetGuestGroupByGuestIdAsync(guestId);
            if (guestGroup == null)
            {
                return NotFound();
            }
            return Ok(guestGroup);
        }
    }
}
