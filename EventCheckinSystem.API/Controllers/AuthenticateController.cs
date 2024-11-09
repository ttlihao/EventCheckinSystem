using EventCheckinSystem.Repo.DTOs;
using EventCheckinSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EventCheckinSystem.API.Controllers
{
    [ApiController]

    [Route("api/[controller]")]
    public class AuthenticateController : Controller
    {
        private readonly IAuthenticateService _authenticateService; 
        public AuthenticateController(IAuthenticateService authenticateService)
        {
            _authenticateService = authenticateService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginRequest)
        {
            var response = await _authenticateService.LoginAsync(loginRequest.UserName, loginRequest.Password);
            if (response == null) return Unauthorized();

            var token = response.VerificationToken;
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict
            };

            Response.Cookies.Append("jwt", token, cookieOptions);

            return Ok(response);
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDTO>> CreateUser([FromBody] UserDTO userDTO)
        {
            var response = await _authenticateService.RegisterAsync(userDTO);
            return Ok(response);
        }

        [HttpGet("login-google")]
        public IActionResult GoogleLogin()
        {
            var properties = new AuthenticationProperties { RedirectUri = Url.Action("GoogleResponse") };

            return Challenge(properties, GoogleDefaults.AuthenticationScheme);
        }


        [HttpGet("google-response")]
        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(GoogleDefaults.AuthenticationScheme);

            if (result?.Succeeded == true)
            {
                var claimsIdentity = result.Principal.Identity as ClaimsIdentity;
                var email = claimsIdentity.FindFirst(ClaimTypes.Email)?.Value;

                if (string.IsNullOrEmpty(email))
                {
                    return Unauthorized();
                }

                // Check if the user exists in the database
                var user = await _authenticateService.GetUserByEmailAsync(email);

                if (user == null)
                {
                    return Unauthorized(new { Message = "User not found. Please contact support." });
                }

                // Generate a token or sign the user in
                var response = await _authenticateService.LoginByGoogleAsync(email);
                var token = response.VerificationToken;
                var cookieOptions = new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict
                };

                Response.Cookies.Append("jwt", token, cookieOptions);

                // Redirect the user to the desired page after login
                return Ok(response);
            }

            return Unauthorized();
        }



    }
}
