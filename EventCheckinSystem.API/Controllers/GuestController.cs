using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Repo.DTOs;
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
    public class GuestController : ControllerBase
    {
        private readonly IGuestServices _guestService;

        public GuestController(IGuestServices guestService)
        {
            _guestService = guestService;
        }

        [HttpGet]
        public async Task<ActionResult<List<GuestDTO>>> GetAllGuests()
        {
            try
            {
                var guests = await _guestService.GetAllGuestsAsync();
                return Ok(guests);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Guest>> GetGuestById(int id)
        {
            try
            {
                var guest = await _guestService.GetGuestByIdAsync(id);
                return Ok(guest);
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
        public async Task<ActionResult> AddGuest([FromBody] GuestDTO newGuest)
        {
            try
            {
                await _guestService.AddGuestAsync(newGuest);
                return CreatedAtAction(nameof(GetGuestById), new { id = newGuest.GuestID }, newGuest);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGuest([FromBody] GuestDTO updatedGuest)
        {
            try
            {
                await _guestService.UpdateGuestAsync(updatedGuest);
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
        public async Task<IActionResult> DeleteGuest(int id)
        {
            try
            {
                await _guestService.DeleteGuestAsync(id);
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

        [HttpGet("group/{groupId}")]
        public async Task<ActionResult<List<GuestDTO>>> GetGuestsByGroupId(int groupId)
        {
            try
            {
                var guests = await _guestService.GetGuestsByGroupIdAsync(groupId);
                return Ok(guests);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
