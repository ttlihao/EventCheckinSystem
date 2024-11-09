using EventCheckinSystem.Repo.DTOs;
using EventCheckinSystem.Repo.DTOs.CreateDTO;
using EventCheckinSystem.Repo.DTOs.Paging;
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
        public async Task<ActionResult<PagedResult<GuestCheckinDTO>>> GetPagedCheckins([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var pageRequest = new PageRequest { PageNumber = pageNumber, PageSize = pageSize };
                var checkins = await _guestCheckinService.GetPagedCheckinsAsync(pageRequest);
                return Ok(checkins);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
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

        [HttpPost("{id}")]
        public async Task<ActionResult<GuestCheckinDTO>> CreateCheckin(int id)
        {
            var createdCheckin = await _guestCheckinService.CreateCheckinAsync(id);
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

    }
}
