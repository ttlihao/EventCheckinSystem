using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCheckinSystem.Services.Services
{
    public class OrganizationServices : IOrganizationServices
    {
        private readonly EventCheckinManagementContext _context;

        public OrganizationServices(EventCheckinManagementContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Organization>> GetAllOrganizationsAsync()
        {
            return await _context.Organizations
                                 .Include(o => o.Events)
                                 .ToListAsync();
        }

        public async Task<Organization> GetOrganizationByIdAsync(int id)
        {
            return await _context.Organizations
                                 .Include(o => o.Events)
                                 .FirstOrDefaultAsync(o => o.OrganizationID == id);
        }

        public async Task<Organization> CreateOrganizationAsync(Organization organization)
        {
            await _context.Organizations.AddAsync(organization);
            await _context.SaveChangesAsync();
            return organization;
        }

        public async Task UpdateOrganizationAsync(Organization updatedOrganization)
        {
            var existingOrganization = await _context.Organizations.FindAsync(updatedOrganization.OrganizationID);

            if (existingOrganization != null)
            {
                existingOrganization.Name = updatedOrganization.Name;
                existingOrganization.Events = updatedOrganization.Events;

                _context.Organizations.Update(existingOrganization);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteOrganizationAsync(int id)
        {
            var organizationToDelete = await _context.Organizations.FindAsync(id);
            if (organizationToDelete != null)
            {
                _context.Organizations.Remove(organizationToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
