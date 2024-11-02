using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Repo.DTOs;
using EventCheckinSystem.Repo.DTOs.CreateDTO;
using EventCheckinSystem.Repo.Repositories.Interfaces;
using EventCheckinSystem.Services.Interfaces;
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
        private readonly IGuestImageServices _guestImageService;

        public GuestImageController(IGuestImageServices guestImageService)
        {
            _guestImageService = guestImageService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GuestImageDTO>>> GetAllGuestImages()
        {
            try
            {
                var guestImages = await _guestImageService.GetAllGuestImagesAsync();
                return Ok(guestImages);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GuestImageDTO>> GetGuestImageById(int id)
        {
            try
            {
                var guestImage = await _guestImageService.GetGuestImageByIdAsync(id);
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
        public async Task<ActionResult<GuestImageDTO>> CreateGuestImage([FromBody] CreateGuestImageDTO newGuestImage)
        {
            try
            {
                var createdImage = await _guestImageService.CreateGuestImageAsync(newGuestImage);
                return CreatedAtAction(nameof(GetGuestImageById), new { id = createdImage.GuestImageID }, createdImage);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGuestImage([FromBody] GuestImageDTO updatedGuestImage)
        {
            try
            {
                await _guestImageService.UpdateGuestImageAsync(updatedGuestImage);
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
                await _guestImageService.DeleteGuestImageAsync(id);
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
