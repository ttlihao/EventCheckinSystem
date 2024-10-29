using AutoMapper;
using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Repo.DTOs;
using EventCheckinSystem.Repo.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCheckinSystem.Repo.Repositories.Implements
{
    public class OrganizationRepo : IOrganizationRepo
    {
        private readonly EventCheckinManagementContext _context;

        public OrganizationRepo(EventCheckinManagementContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Organization>> GetAllOrganizationsAsync()
        {
            var organizations = await _context.Organizations
                .Include(o => o.Events)
                    .ThenInclude(e => e.GuestGroups)
                .Include(o => o.Events)
                    .ThenInclude(e => e.UserEvents)
                .ToListAsync();

            return organizations;
        }

        public async Task<Organization> GetOrganizationByIdAsync(int id)
        {
            var organizationEntity = await _context.Organizations
                .Include(o => o.Events)
                    .ThenInclude(e => e.GuestGroups)
                .Include(o => o.Events)
                    .ThenInclude(e => e.UserEvents)
                .FirstOrDefaultAsync(o => o.OrganizationID == id);

            return organizationEntity;
        }

        public async Task<Organization> CreateOrganizationAsync(Organization newOrganization)
        {
            await _context.Organizations.AddAsync(newOrganization);
            await _context.SaveChangesAsync();
            return newOrganization;
        }

        public async Task<bool> UpdateOrganizationAsync(Organization updatedOrganizationDto)
        {
            bool isSuccess = false;
            try
            {
                var existingOrganiztion = await _context.Organizations.FirstOrDefaultAsync(e => e.OrganizationID == updatedOrganizationDto.OrganizationID);
                if (existingOrganiztion != null)
                {
                    _context.Entry(existingOrganiztion).State = EntityState.Detached; // Detach the existing entity
                    _context.Organizations.Attach(updatedOrganizationDto);
                    _context.Entry(updatedOrganizationDto).State = EntityState.Modified; // Mark as modified
                    var changes = await _context.SaveChangesAsync();
                    isSuccess = changes > 0; // Return true if changes were made
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Error updating guest check-in: {e.Message}");
            }
            return isSuccess;
        }

        public async Task<bool> DeleteOrganizationAsync(int id)
        {
            var organizationToDelete = await _context.Organizations.FindAsync(id);
            if (organizationToDelete != null)
            {
                    organizationToDelete.IsActive = false;
                    organizationToDelete.IsDelete = true;
                    _context.Organizations.Update(organizationToDelete);
                    await _context.SaveChangesAsync();
                    return true;
            }
                return false;
        }
    }
}
