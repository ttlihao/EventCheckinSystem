using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Services.Interfaces;

namespace EventCheckinSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestCheckinController : ControllerBase
    {
        private readonly IGuestCheckinServices _guestCheckinServices;

        public GuestCheckinController(IGuestCheckinServices guestCheckinServices)
        {
            _guestCheckinServices = guestCheckinServices;
        }

        // GET: api/GuestCheckin
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GuestCheckin>>> GetAllCheckins()
        {
            var checkins = await _guestCheckinServices.GetAllCheckinsAsync();
            return Ok(checkins);
        }

        // GET: api/GuestCheckin/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<GuestCheckin>> GetCheckinById(int id)
        {
            var checkin = await _guestCheckinServices.GetCheckinByIdAsync(id);

            if (checkin == null)
            {
                return NotFound($"Checkin with ID {id} not found.");
            }

            return Ok(checkin);
        }

        // POST: api/GuestCheckin
        [HttpPost]
        public async Task<IActionResult> CreateCheckin([FromBody] GuestCheckin guestCheckin)
        {
            if (guestCheckin == null)
            {
                return BadRequest("Guest check-in data is null.");
            }

            try
            {
                var createdCheckin = await _guestCheckinServices.CreateCheckinAsync(guestCheckin);
                return CreatedAtAction(nameof(GetCheckinById), new { id = createdCheckin.GuestCheckinID }, createdCheckin);
            }
            catch
            {
                return StatusCode(500, "Internal server error while creating check-in.");
            }
        }

        // PUT: api/GuestCheckin/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCheckin(int id, [FromBody] GuestCheckin guestCheckin)
        {
            if (id != guestCheckin.GuestCheckinID)
            {
                return BadRequest("Checkin ID mismatch.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _guestCheckinServices.UpdateCheckinAsync(guestCheckin);
            return NoContent();
        }

        // DELETE: api/GuestCheckin/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCheckin(int id)
        {
            await _guestCheckinServices.DeleteCheckinAsync(id);
            return NoContent();
        }

        // POST: api/GuestCheckin/checkin/{guestId}
        [HttpPost("checkin/{guestId}")]
        public async Task<IActionResult> CheckinGuestByGuestId(int guestId, [FromBody] string createdBy)
        {
            if (string.IsNullOrEmpty(createdBy))
            {
                return BadRequest("CreatedBy cannot be null or empty.");
            }

            try
            {
                var checkin = await _guestCheckinServices.CheckinGuestByIdAsync(guestId, createdBy);
                return CreatedAtAction(nameof(GetCheckinById), new { id = checkin.GuestCheckinID }, checkin);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
