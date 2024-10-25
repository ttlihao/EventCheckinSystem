using EventCheckinSystem.Services.Interfaces;
using EventCheckinSystem.Repo.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GuestCheckinDTO>>> GetAllCheckins()
        {
            var checkins = await _guestCheckinServices.GetAllCheckinsAsync();
            return Ok(checkins);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GuestCheckinDTO>> GetCheckinById(int id)
        {
            var checkin = await _guestCheckinServices.GetCheckinByIdAsync(id);
            if (checkin == null)
                return NotFound();
            return Ok(checkin);
        }

        [HttpPost]
        public async Task<ActionResult<GuestCheckinDTO>> CreateCheckin(GuestCheckinDTO guestCheckinDto)
        {
            var createdBy = User.Identity.Name;

            if (string.IsNullOrEmpty(createdBy))
            {
                return BadRequest("The 'createdBy' information is missing.");
            }

            var createdCheckin = await _guestCheckinServices.CreateCheckinAsync(guestCheckinDto, createdBy);
            return CreatedAtAction(nameof(GetCheckinById), new { id = createdCheckin.GuestCheckinID }, createdCheckin);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCheckin(int id, GuestCheckinDTO guestCheckinDto)
        {
            if (id != guestCheckinDto.GuestCheckinID)
                return BadRequest();

            await _guestCheckinServices.UpdateCheckinAsync(guestCheckinDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCheckin(int id)
        {
            await _guestCheckinServices.DeleteCheckinAsync(id);
            return NoContent();
        }

        [HttpPost("checkin/{guestId}")]
        public async Task<ActionResult<GuestCheckinDTO>> CheckinGuestById(int guestId, [FromBody] string createdBy)
        {
            var checkin = await _guestCheckinServices.CheckinGuestByIdAsync(guestId, createdBy);
            return Ok(checkin);
        }
    }
}
