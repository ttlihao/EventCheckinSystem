using EventCheckinSystem.Repo.DTOs;
using EventCheckinSystem.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCheckinSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GuestImageController : ControllerBase
    {
        private readonly IGuestImageServices _guestImageServices;

        public GuestImageController(IGuestImageServices guestImageServices)
        {
            _guestImageServices = guestImageServices;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GuestImageDTO>>> GetAllGuestImages()
        {
            var guestImages = await _guestImageServices.GetAllGuestImagesAsync();
            return Ok(guestImages);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GuestImageDTO>> GetGuestImageById(int id)
        {
            var guestImage = await _guestImageServices.GetGuestImageByIdAsync(id);
            if (guestImage == null)
            {
                return NotFound();
            }
            return Ok(guestImage);
        }

        [HttpPost]
        public async Task<ActionResult<GuestImageDTO>> CreateGuestImage(GuestImageDTO guestImageDto)
        {
            var createdGuestImage = await _guestImageServices.CreateGuestImageAsync(guestImageDto);
            return CreatedAtAction(nameof(GetGuestImageById), new { id = createdGuestImage.GuestImageID }, createdGuestImage);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGuestImage(int id, GuestImageDTO guestImageDto)
        {
            if (id != guestImageDto.GuestImageID)
            {
                return BadRequest();
            }

            await _guestImageServices.UpdateGuestImageAsync(guestImageDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGuestImage(int id)
        {
            await _guestImageServices.DeleteGuestImageAsync(id);
            return NoContent();
        }
    }
}
