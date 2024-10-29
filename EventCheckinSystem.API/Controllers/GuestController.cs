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
    public class GuestController : ControllerBase
    {
        private readonly IGuestRepo _guestRepo;

        public GuestController(IGuestRepo guestRepo)
        {
            _guestRepo = guestRepo;
        }

        [HttpGet]
        public async Task<ActionResult<List<Guest>>> GetAllGuests()
        {
            try
            {
                var guests = await _guestRepo.GetAllGuestsAsync();
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
                var guest = await _guestRepo.GetGuestByIdAsync(id);
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
        public async Task<ActionResult> AddGuest([FromBody] Guest newGuest)
        {
            try
            {
                await _guestRepo.AddGuestAsync(newGuest);
                return CreatedAtAction(nameof(GetGuestById), new { id = newGuest.GuestID }, newGuest);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGuest([FromBody] Guest updatedGuest)
        {
            try
            {
                await _guestRepo.UpdateGuestAsync(updatedGuest);
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
                await _guestRepo.DeleteGuestAsync(id);
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
        public async Task<ActionResult<List<Guest>>> GetGuestsByGroupId(int groupId)
        {
            try
            {
                var guests = await _guestRepo.GetGuestsByGroupIdAsync(groupId);
                return Ok(guests);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
