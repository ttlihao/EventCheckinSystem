
using EventCheckinSystem.Repo.DTOs;
using EventCheckinSystem.Repo.DTOs.CreateDTO;
using EventCheckinSystem.Repo.DTOs.Paging;
using EventCheckinSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
        public async Task<ActionResult<PagedResult<GuestDTO>>> GetAllGuests([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            try
            {
                var pageRequest = new PageRequest { PageNumber = pageNumber, PageSize = pageSize };
                var guests = await _guestService.GetPagedGuestsAsync(pageRequest);
                return Ok(guests);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GuestDTO>> GetGuestById(int id)
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
        [HttpGet("get-by-name/{name}")]
        public async Task<ActionResult<GuestDTO>> GetGuestByName(string name)
        {
            try
            {
                var guest = await _guestService.GetGuestByNameAsync(name);
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
        [HttpGet("get-by-eventid/{id}")]
        public async Task<ActionResult<GuestDTO>> GetGuestByEventId(int id)
        {
            try
            {
                var guest = await _guestService.GetGuestsByEventIdAsync(id);
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
        [HttpGet("get-total-by-eventid/{id}")]
        public async Task<ActionResult<int>> GetTotalGuestByEventId(int id)
        {
            try
            {
                var guest = await _guestService.GetGuestsByEventIdAsync(id);
                return Ok(guest.Count());
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
        public async Task<ActionResult> AddGuest([FromForm] CreateGuestDTO newGuest, IFormFile imageFile)
        {
            try
            {
                var response = await _guestService.AddGuestAsync(newGuest, imageFile);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateGuest([FromForm] GuestDTO updatedGuest, IFormFile imageFile = null)
        {
            try
            {
                var result = await _guestService.UpdateGuestAsync(updatedGuest, imageFile);

                if (result)
                {
                    return Ok(new { Message = "Guest updated successfully" });
                }
                else
                {
                    return NotFound("Guest not found or update failed");
                }
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
