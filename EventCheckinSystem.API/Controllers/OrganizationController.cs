using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Services.Interfaces;

namespace EventCheckinSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationServices _organizationServices;

        public OrganizationController(IOrganizationServices organizationServices)
        {
            _organizationServices = organizationServices;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Organization>>> GetAllOrganizations()
        {
            var organizations = await _organizationServices.GetAllOrganizationsAsync();
            return Ok(organizations);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Organization>> GetOrganizationById(int id)
        {
            var organization = await _organizationServices.GetOrganizationByIdAsync(id);

            if (organization == null)
            {
                return NotFound($"Organization with ID {id} not found.");
            }

            return Ok(organization);
        }

        [HttpPost]
        public async Task<ActionResult> AddOrganization([FromBody] Organization organization)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdOrganization = await _organizationServices.CreateOrganizationAsync(organization);
            return CreatedAtAction(nameof(GetOrganizationById), new { id = createdOrganization.OrganizationID }, createdOrganization);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrganization(int id, [FromBody] Organization organization)
        {
            if (id != organization.OrganizationID)
            {
                return BadRequest("Organization ID mismatch.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _organizationServices.UpdateOrganizationAsync(organization);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrganization(int id)
        {
            await _organizationServices.DeleteOrganizationAsync(id);
            return NoContent();
        }
    }
}
