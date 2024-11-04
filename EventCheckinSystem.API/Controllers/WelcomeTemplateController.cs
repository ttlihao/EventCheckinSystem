using EventCheckinSystem.Repo.DTOs;
using EventCheckinSystem.Repo.DTOs.CreateDTO;
using EventCheckinSystem.Repo.Repositories.Interfaces;
using EventCheckinSystem.Services.Interfaces;
using EventCheckinSystem.Services.Services;
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
        private readonly IWelcomeTemplateServices _welcomeTemplateService;

        public WelcomeTemplateController(IWelcomeTemplateServices welcomeTemplateRepo)
        {
            _welcomeTemplateService = welcomeTemplateRepo;
        }

        [HttpGet("paged")]
        public async Task<IActionResult> GetPagedWelcomeTemplates(int pageNumber = 1, int pageSize = 10)
        {
            var (templates, totalCount) = await _welcomeTemplateService.GetWelcomeTemplatesPagedAsync(pageNumber, pageSize);
            return Ok(new
            {
                TotalCount = totalCount,
                PageSize = pageSize,
                PageNumber = pageNumber,
                Templates = templates
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<WelcomeTemplateDTO>> GetWelcomeTemplateById(int id)
        {
            try
            {
                var template = await _welcomeTemplateService.GetWelcomeTemplateByIdAsync(id);
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
        public async Task<ActionResult<WelcomeTemplateDTO>> CreateWelcomeTemplate([FromBody] CreateWelcomeTemplateDTO newTemplate)
        {
            try
            {
                var createdTemplate = await _welcomeTemplateService.CreateWelcomeTemplateAsync(newTemplate);
                return CreatedAtAction(nameof(GetWelcomeTemplateById), new { id = createdTemplate.WelcomeTemplateID }, createdTemplate);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateWelcomeTemplate([FromBody] WelcomeTemplateDTO updatedTemplate)
        {
            try
            {
                await _welcomeTemplateService.UpdateWelcomeTemplateAsync(updatedTemplate);
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
                await _welcomeTemplateService.DeleteWelcomeTemplateAsync(id);
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
                var templates = await _welcomeTemplateService.GetWelcomeTemplatesByGuestGroupAsync(guestGroupId);
                return Ok(templates);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
