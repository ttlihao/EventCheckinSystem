using EventCheckinSystem.Repo.DTOs;
using EventCheckinSystem.Repo.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCheckinSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GuestCheckinController : ControllerBase
    {
        private readonly IGuestCheckinRepo _guestCheckinRepo;

        public GuestCheckinController(IGuestCheckinRepo guestCheckinRepo)
        {
            _guestCheckinRepo = guestCheckinRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GuestCheckinDTO>>> GetAllCheckins()
        {
            var checkins = await _guestCheckinRepo.GetAllCheckinsAsync();
            return Ok(checkins);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GuestCheckinDTO>> GetCheckinById(int id)
        {
            var checkin = await _guestCheckinRepo.GetCheckinByIdAsync(id);
            if (checkin == null)
            {
                return NotFound();
            }
            return Ok(checkin);
        }

        [HttpPost]
        public async Task<ActionResult<GuestCheckinDTO>> CreateCheckin([FromBody] GuestCheckinDTO checkinDto, [FromQuery] string createdBy)
        {
            var createdCheckin = await _guestCheckinRepo.CreateCheckinAsync(checkinDto, createdBy);
            return CreatedAtAction(nameof(GetCheckinById), new { id = createdCheckin.GuestCheckinID }, createdCheckin);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCheckin([FromBody] GuestCheckinDTO checkinDto)
        {
            await _guestCheckinRepo.UpdateCheckinAsync(checkinDto);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCheckin(int id)
        {
            await _guestCheckinRepo.DeleteCheckinAsync(id);
            return Ok();
        }

        [HttpPost("checkin")]
        public async Task<ActionResult<GuestCheckinDTO>> CheckinGuest([FromQuery] int guestId, [FromQuery] string createdBy)
        {
            var checkin = await _guestCheckinRepo.CheckinGuestByIdAsync(guestId, createdBy);
            return Ok(checkin);
        }
    }
}
