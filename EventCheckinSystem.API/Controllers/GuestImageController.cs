using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Services.Interfaces;

namespace EventCheckinSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GuestImageController : ControllerBase
    {
        private readonly IGuestImageServices _guestImageServices;

        public GuestImageController(IGuestImageServices guestImageServices)
        {
            _guestImageServices = guestImageServices;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GuestImage>>> GetAllGuestImages()
        {
            var images = await _guestImageServices.GetAllGuestImagesAsync();
            return Ok(images);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GuestImage>> GetGuestImageById(int id)
        {
            var image = await _guestImageServices.GetGuestImageByIdAsync(id);

            if (image == null)
            {
                return NotFound($"Guest image with ID {id} not found.");
            }

            return Ok(image);
        }

        [HttpPost]
        public async Task<ActionResult> AddGuestImage([FromBody] GuestImage guestImage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdImage = await _guestImageServices.CreateGuestImageAsync(guestImage);
            return CreatedAtAction(nameof(GetGuestImageById), new { id = createdImage.GuestImageID }, createdImage);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGuestImage(int id, [FromBody] GuestImage guestImage)
        {
            if (id != guestImage.GuestImageID)
            {
                return BadRequest("Guest image ID mismatch.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _guestImageServices.UpdateGuestImageAsync(guestImage);
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
