using EventCheckinSystem.Repo.DTOs;
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
    public class WelcomeTemplateController : ControllerBase
    {
        private readonly IWelcomeTemplateRepo _welcomeTemplateRepo;

        public WelcomeTemplateController(IWelcomeTemplateRepo welcomeTemplateRepo)
        {
            _welcomeTemplateRepo = welcomeTemplateRepo;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<WelcomeTemplateDTO>>> GetAllWelcomeTemplates()
        {
            try
            {
                var templates = await _welcomeTemplateRepo.GetAllWelcomeTemplatesAsync();
                return Ok(templates);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WelcomeTemplateDTO>> GetWelcomeTemplateById(int id)
        {
            try
            {
                var template = await _welcomeTemplateRepo.GetWelcomeTemplateByIdAsync(id);
                if (template == null)
                {
                    return NotFound($"WelcomeTemplate with ID {id} not found.");
                }
                return Ok(template);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<WelcomeTemplateDTO>> CreateWelcomeTemplate([FromBody] WelcomeTemplateDTO newTemplate, [FromQuery] string createdBy)
        {
            try
            {
                var createdTemplate = await _welcomeTemplateRepo.CreateWelcomeTemplateAsync(newTemplate, createdBy);
                return CreatedAtAction(nameof(GetWelcomeTemplateById), new { id = createdTemplate.WelcomeTemplateID }, createdTemplate);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateWelcomeTemplate([FromBody] WelcomeTemplateDTO updatedTemplate, [FromQuery] string updatedBy)
        {
            try
            {
                await _welcomeTemplateRepo.UpdateWelcomeTemplateAsync(updatedTemplate, updatedBy);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWelcomeTemplate(int id)
        {
            try
            {
                await _welcomeTemplateRepo.DeleteWelcomeTemplateAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("guestgroup/{guestGroupId}")]
        public async Task<ActionResult<IEnumerable<WelcomeTemplateDTO>>> GetWelcomeTemplatesByGuestGroup(int guestGroupId)
        {
            try
            {
                var templates = await _welcomeTemplateRepo.GetWelcomeTemplatesByGuestGroupAsync(guestGroupId);
                return Ok(templates);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
