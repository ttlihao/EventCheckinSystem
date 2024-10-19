using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Repo.DTOs;
using EventCheckinSystem.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCheckinSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // Optional: Ensure the user is authenticated
    public class WelcomeTemplateController : ControllerBase
    {
        private readonly IWelcomeTemplateServices _welcomeTemplateServices;

        public WelcomeTemplateController(IWelcomeTemplateServices welcomeTemplateServices)
        {
            _welcomeTemplateServices = welcomeTemplateServices;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WelcomeTemplateDTO>>> GetAll()
        {
            var templates = await _welcomeTemplateServices.GetAllWelcomeTemplatesAsync();
            return Ok(templates);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WelcomeTemplateDTO>> GetById(int id)
        {
            var template = await _welcomeTemplateServices.GetWelcomeTemplateByIdAsync(id);
            if (template == null) return NotFound();
            return Ok(template);
        }

        [HttpPost]
        public async Task<ActionResult<WelcomeTemplate>> Create([FromBody] WelcomeTemplateDTO welcomeTemplateDto)
        {
            var createdBy = User.Identity.Name; // Get the logged-in user's name
            var welcomeTemplate = await _welcomeTemplateServices.CreateWelcomeTemplateAsync(welcomeTemplateDto, createdBy);
            return CreatedAtAction(nameof(GetById), new { id = welcomeTemplate.WelcomeTemplateID }, welcomeTemplate);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] WelcomeTemplateDTO welcomeTemplateDto)
        {
            if (id != welcomeTemplateDto.WelcomeTemplateID) return BadRequest();

            var updatedBy = User.Identity.Name; // Get the logged-in user's name
            await _welcomeTemplateServices.UpdateWelcomeTemplateAsync(welcomeTemplateDto, updatedBy);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _welcomeTemplateServices.DeleteWelcomeTemplateAsync(id);
            return NoContent();
        }

        [HttpGet("guestGroup/{guestGroupId}")]
        public async Task<ActionResult<IEnumerable<WelcomeTemplateDTO>>> GetByGuestGroup(int guestGroupId)
        {
            var templates = await _welcomeTemplateServices.GetWelcomeTemplatesByGuestGroupAsync(guestGroupId);
            return Ok(templates);
        }
    }
}
