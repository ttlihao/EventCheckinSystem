using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Repo.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCheckinSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GuestImageController : ControllerBase
    {
        private readonly IGuestImageRepo _guestImageRepo;

        public GuestImageController(IGuestImageRepo guestImageRepo)
        {
            _guestImageRepo = guestImageRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GuestImage>>> GetAllGuestImages()
        {
            try
            {
                var guestImages = await _guestImageRepo.GetAllGuestImagesAsync();
                return Ok(guestImages);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GuestImage>> GetGuestImageById(int id)
        {
            try
            {
                var guestImage = await _guestImageRepo.GetGuestImageByIdAsync(id);
                return Ok(guestImage);
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<GuestImage>> CreateGuestImage([FromBody] GuestImage newGuestImage)
        {
            try
            {
                var createdImage = await _guestImageRepo.CreateGuestImageAsync(newGuestImage);
                return CreatedAtAction(nameof(GetGuestImageById), new { id = createdImage.GuestImageID }, createdImage);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGuestImage([FromBody] GuestImage updatedGuestImage)
        {
            try
            {
                await _guestImageRepo.UpdateGuestImageAsync(updatedGuestImage);
                return Ok();
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGuestImage(int id)
        {
            try
            {
                await _guestImageRepo.DeleteGuestImageAsync(id);
                return Ok();
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
