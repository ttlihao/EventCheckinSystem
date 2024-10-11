using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Repo.DTOs;
using EventCheckinSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCheckinSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestController : ControllerBase
    {
        private readonly IGuestServices _guestServices;

        public GuestController(IGuestServices guestServices)
        {
            _guestServices = guestServices;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GuestDTO>>> GetAllGuests()
        {
            var guests = await _guestServices.GetAllGuestsAsync();
            return Ok(guests);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GuestDTO>> GetGuestById(int id)
        {
            var guest = await _guestServices.GetGuestByIdAsync(id);
            if (guest == null)
            {
                return NotFound();
            }
            return Ok(guest);
        }

        [HttpPost]
        public async Task<ActionResult<GuestDTO>> AddGuest([FromBody] Guest guest)
        {
            string createdBy = User.Identity.Name;
            await _guestServices.AddGuestAsync(guest, createdBy);
            return CreatedAtAction(nameof(GetGuestById), new { id = guest.GuestID }, guest);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGuest(int id, [FromBody] Guest guest)
        {
            if (id != guest.GuestID)
            {
                return BadRequest();
            }
            string updatedBy = User.Identity.Name;
            await _guestServices.UpdateGuestAsync(guest, updatedBy);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGuest(int id)
        {
            await _guestServices.DeleteGuestAsync(id);
            return NoContent();
        }

        [HttpGet("group/{guestGroupId}")]
        public async Task<ActionResult<IEnumerable<GuestDTO>>> GetGuestsByGroupId(int guestGroupId)
        {
            var guests = await _guestServices.GetGuestsByGroupIdAsync(guestGroupId);
            return Ok(guests);
        }
    }
}
