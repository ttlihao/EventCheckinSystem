using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Services.Interfaces;

namespace EventCheckinSystem.API.Controllers
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
        public async Task<ActionResult<IEnumerable<GuestGroup>>> GetAllGuestGroups()
        {
            var groups = await _guestGroupServices.GetAllGuestGroupsAsync();
            return Ok(groups);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<GuestGroup>> GetGuestGroupById(int id)
        {
            var group = await _guestGroupServices.GetGuestGroupByIdAsync(id);

            if (group == null)
            {
                return NotFound($"Guest group with ID {id} not found.");
            }

            return Ok(group);
        }

        [HttpPost]
        public async Task<ActionResult> AddGuestGroup([FromBody] GuestGroup guestGroup)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdGroup = await _guestGroupServices.CreateGuestGroupAsync(guestGroup);
            return CreatedAtAction(nameof(GetGuestGroupById), new { id = createdGroup.GuestGroupID }, createdGroup);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGuestGroup(int id, [FromBody] GuestGroup guestGroup)
        {
            if (id != guestGroup.GuestGroupID)
            {
                return BadRequest("Guest group ID mismatch.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _guestGroupServices.UpdateGuestGroupAsync(guestGroup);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGuestGroup(int id)
        {
            await _guestGroupServices.DeleteGuestGroupAsync(id);
            return NoContent();
        }

        [HttpGet("guest/{guestId}")]
        public async Task<ActionResult<GuestGroup>> GetGuestGroupByGuestId(int guestId)
        {
            var guestGroup = await _guestGroupServices.GetGuestGroupByGuestIdAsync(guestId);

            if (guestGroup == null)
            {
                return NotFound($"No guest group found for guest ID {guestId}.");
            }

            return Ok(guestGroup);
        }
    }
}
