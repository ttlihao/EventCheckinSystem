using EventCheckinSystem.Services.Interfaces;
using EventCheckinSystem.Repo.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCheckinSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WelcomeTemplateController : ControllerBase
    {
        private readonly IWelcomeTemplateServices _welcomeTemplateServices;

        public WelcomeTemplateController(IWelcomeTemplateServices welcomeTemplateServices)
        {
            _welcomeTemplateServices = welcomeTemplateServices;
        }

        // GET: api/WelcomeTemplate
        [HttpGet]
        public async Task<IActionResult> GetAllWelcomeTemplates()
        {
            var templates = await _welcomeTemplateServices.GetAllWelcomeTemplatesAsync();
            return Ok(templates);
        }

        // GET: api/WelcomeTemplate/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetWelcomeTemplate(int id)
        {
            var template = await _welcomeTemplateServices.GetWelcomeTemplateByIdAsync(id);
            if (template == null)
            {
                return NotFound($"WelcomeTemplate with ID {id} not found.");
            }

            return Ok(template);
        }

        // POST: api/WelcomeTemplate
        [HttpPost]
        public async Task<IActionResult> CreateWelcomeTemplate([FromBody] WelcomeTemplate welcomeTemplate)
        {
            if (welcomeTemplate == null)
            {
                return BadRequest("WelcomeTemplate data is null.");
            }

            var createdTemplate = await _welcomeTemplateServices.CreateWelcomeTemplateAsync(welcomeTemplate);
            return CreatedAtAction(nameof(GetWelcomeTemplate), new { id = createdTemplate.WelcomeTemplateID }, createdTemplate);
        }

        // PUT: api/WelcomeTemplate/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateWelcomeTemplate(int id, [FromBody] WelcomeTemplate welcomeTemplate)
        {
            if (id != welcomeTemplate.WelcomeTemplateID)
            {
                return BadRequest("ID mismatch.");
            }

            await _welcomeTemplateServices.UpdateWelcomeTemplateAsync(welcomeTemplate);
            return NoContent();
        }

        // DELETE: api/WelcomeTemplate/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWelcomeTemplate(int id)
        {
            await _welcomeTemplateServices.DeleteWelcomeTemplateAsync(id);
            return NoContent();
        }

        // GET: api/WelcomeTemplate/guestgroup/{guestGroupId}
        [HttpGet("guestgroup/{guestGroupId}")]
        public async Task<IActionResult> GetWelcomeTemplatesByGuestGroup(int guestGroupId)
        {
            var templates = await _welcomeTemplateServices.GetWelcomeTemplatesByGuestGroupAsync(guestGroupId);

            if (templates == null || !templates.Any())
            {
                return NotFound($"No welcome templates found for Guest Group ID {guestGroupId}.");
            }

            return Ok(templates);
        }

    }
}
