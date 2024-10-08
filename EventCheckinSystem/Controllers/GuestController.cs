using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Services;
using EventCheckinSystem.Services.Interfaces;

namespace EventCheckinSystem.API.Controllers
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
        public async Task<ActionResult<IEnumerable<Guest>>> GetAllGuests()
        {
            var guests = await _guestServices.GetAllGuestsAsync();
            return Ok(guests);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Guest>> GetGuestById(int id)
        {
            var guest = await _guestServices.GetGuestByIdAsync(id);

            if (guest == null)
            {
                return NotFound($"Guest with ID {id} not found.");
            }

            return Ok(guest);
        }

        [HttpPost]
        public async Task<ActionResult> AddGuest([FromBody] Guest guest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _guestServices.AddGuestAsync(guest);
                return CreatedAtAction(nameof(GetGuestById), new { id = guest.GuestID }, guest);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGuest(int id, [FromBody] Guest guest)
        {
            if (id != guest.GuestID)
            {
                return BadRequest("Guest ID mismatch.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _guestServices.UpdateGuestAsync(guest);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // DELETE: api/Guest/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGuest(int id)
        {
            try
            {
                await _guestServices.DeleteGuestAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("group/{guestGroupId}")]
        public async Task<ActionResult<IEnumerable<Guest>>> GetGuestsByGroupId(int guestGroupId)
        {
            try
            {
                var guests = await _guestServices.GetGuestsByGroupIdAsync(guestGroupId);

                if (guests == null || !guests.Any())
                {
                    return NotFound($"No guests found for group ID {guestGroupId}.");
                }

                return Ok(guests);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
