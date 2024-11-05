using EventCheckinSystem.Repo.DTOs;
using EventCheckinSystem.Repo.DTOs.CreateDTO;
using EventCheckinSystem.Services.Interfaces;
using EventCheckinSystem.Services.Services;
using Microsoft.AspNetCore.Mvc;

namespace EventCheckinSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IAuthenticateService _authenticateService;
        public UserController(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAllUser()
        {
            var response = await _authenticateService.GetUsers();
            return Ok(response);
        }
        [HttpGet("get-by-id")]
        public async Task<IActionResult> GetAllUserById([FromQuery] string userId)
        {
            var response = await _authenticateService.GetUserByIdAsync(userId);
            return Ok(response);
        }
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateUserDTO user)
        {
            var response = await _authenticateService.UpdateUserAsync(user);
            return Ok(response);
        }
        [HttpPut("change-password")]
        public async Task<IActionResult> ChangeUserPassword([FromQuery] ChangePasswordDTO request)
        {
            var response = await _authenticateService.ChangeUserPasswordAsync(request);
            return Ok(response);
        }

        [HttpDelete("deactive")]
        public async Task<IActionResult> Deactiveuser([FromQuery] string userId)
        {
            var response = await _authenticateService.DeactiveUserAsync(userId);
            return Ok(response);
        }
        [HttpPut("active")]
        public async Task<IActionResult> Activeuser([FromQuery] string userId)
        {
            var response = await _authenticateService.ActiveUserAsync(userId);
            return Ok(response);
        }
    }
}
