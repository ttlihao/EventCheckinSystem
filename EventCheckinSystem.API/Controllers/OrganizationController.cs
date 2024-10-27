using EventCheckinSystem.Repo.DTOs;
using EventCheckinSystem.Repo.Repositories.Interfaces;
using EventCheckinSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCheckinSystem.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationServices _organizationServices;

        public OrganizationController(IOrganizationServices organizationServices)
        {
            _organizationServices = organizationServices;
        }

        // GET: api/organization
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrganizationDTO>>> GetAllOrganizations()
        {
            var organizations = await _organizationServices.GetAllOrganizationsAsync();
            return Ok(organizations);
        }

        // GET: api/organization/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<OrganizationDTO>> GetOrganizationById(int id)
        {
            var organization = await _organizationServices.GetOrganizationByIdAsync(id);
            if (organization == null) return NotFound();

            return Ok(organization);
        }

        // POST: api/organization
        [HttpPost]
        public async Task<ActionResult<OrganizationDTO>> CreateOrganization([FromBody] OrganizationDTO newOrganizationDto)
        {
            if (newOrganizationDto == null) return BadRequest();

            var createdOrganization = await _organizationServices.CreateOrganizationAsync(newOrganizationDto);
            return CreatedAtAction(nameof(GetOrganizationById), new { id = createdOrganization.Name }, createdOrganization);
        }

        // PUT: api/organization
        [HttpPut]
        public async Task<IActionResult> UpdateOrganization([FromBody] OrganizationDTO updatedOrganizationDto)
        {
            if (updatedOrganizationDto == null) return BadRequest();

            await _organizationServices.UpdateOrganizationAsync(updatedOrganizationDto);
            return NoContent();
        }

        // DELETE: api/organization/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrganization(int id)
        {
            await _organizationServices.DeleteOrganizationAsync(id);
            return NoContent();
        }
    }
}
