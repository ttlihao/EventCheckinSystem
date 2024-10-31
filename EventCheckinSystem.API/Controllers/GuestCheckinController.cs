using EventCheckinSystem.Repo.DTOs;
using EventCheckinSystem.Repo.DTOs.CreateDTO;
using EventCheckinSystem.Services.Interfaces;
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
        private readonly IGuestCheckinServices _guestCheckinService;

        public GuestCheckinController(IGuestCheckinServices guestCheckinService)
        {
            _guestCheckinService = guestCheckinService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GuestCheckinDTO>>> GetAllCheckins()
        {
            var checkins = await _guestCheckinService.GetAllCheckinsAsync();
            return Ok(checkins);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GuestCheckinDTO>> GetCheckinById(int id)
        {
            var checkin = await _guestCheckinService.GetCheckinByIdAsync(id);
            if (checkin == null)
            {
                return NotFound();
            }
            return Ok(checkin);
        }

        [HttpPost]
        public async Task<ActionResult<GuestCheckinDTO>> CreateCheckin([FromBody] CreateGuestCheckinDTO checkinDto)
        {
            var createdCheckin = await _guestCheckinService.CreateCheckinAsync(checkinDto);
            return CreatedAtAction(nameof(GetCheckinById), new { id = createdCheckin.GuestCheckinID }, createdCheckin);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCheckin([FromBody] GuestCheckinDTO checkinDto)
        {
            return Ok(await _guestCheckinService.UpdateCheckinAsync(checkinDto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCheckin(int id)
        {
            return Ok(await _guestCheckinService.DeleteCheckinAsync(id));
        }

        [HttpPost("checkin")]
        public async Task<ActionResult<GuestCheckinDTO>> CheckinGuest([FromQuery] int guestId)
        {
            var checkin = await _guestCheckinService.CheckinGuestByIdAsync(guestId);
            return Ok(checkin);
        }
    }
}
