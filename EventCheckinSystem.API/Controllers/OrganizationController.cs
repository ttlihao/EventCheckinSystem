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
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationServices _organizationService;

        public OrganizationController(IOrganizationServices organizationService)
        {
            _organizationService = organizationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrganizationDTO>>> GetAllOrganizations()
        {
            try
            {
                var organizations = await _organizationService.GetAllOrganizationsAsync();
                return Ok(organizations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrganizationDTO>> GetOrganizationById(int id)
        {
            try
            {
                var organization = await _organizationService.GetOrganizationByIdAsync(id);
                if (organization == null)
                {
                    return NotFound($"Organization with ID {id} not found.");
                }
                return Ok(organization);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<OrganizationDTO>> CreateOrganization([FromBody] CreateOrganizationDTO newOrganization)
        {
            try
            {
                var createdOrganization = await _organizationService.CreateOrganizationAsync(newOrganization);
                return Ok(createdOrganization);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateOrganization([FromBody] OrganizationDTO updatedOrganization)
        {
            try
            {
                await _organizationService.UpdateOrganizationAsync(updatedOrganization);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrganization(int id)
        {
            try
            {
                await _organizationService.DeleteOrganizationAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
