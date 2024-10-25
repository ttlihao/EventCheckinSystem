using AutoMapper;
using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Repo.DTOs;
using EventCheckinSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCheckinSystem.Services.Services
{
    public class OrganizationServices : IOrganizationServices
    {
        private readonly EventCheckinManagementContext _context;
        private readonly IMapper _mapper;

        public OrganizationServices(EventCheckinManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrganizationDTO>> GetAllOrganizationsAsync()
        {
            var organizations = await _context.Organizations
                .Include(o => o.Events)
                    .ThenInclude(e => e.GuestGroups)
                .Include(o => o.Events)
                    .ThenInclude(e => e.UserEvents)
                .ToListAsync();

            return _mapper.Map<IEnumerable<OrganizationDTO>>(organizations);
        }

        public async Task<OrganizationDTO> GetOrganizationByIdAsync(int id)
        {
            var organizationEntity = await _context.Organizations
                .Include(o => o.Events)
                    .ThenInclude(e => e.GuestGroups)
                .Include(o => o.Events)
                    .ThenInclude(e => e.UserEvents)
                .FirstOrDefaultAsync(o => o.OrganizationID == id);

            return _mapper.Map<OrganizationDTO>(organizationEntity);
        }

        public async Task<OrganizationDTO> CreateOrganizationAsync(OrganizationDTO newOrganizationDto)
        {
            var newOrganization = _mapper.Map<Organization>(newOrganizationDto);
            newOrganization.CreatedBy = "User";
            newOrganization.LastUpdatedBy = "User";
            await _context.Organizations.AddAsync(newOrganization);
            await _context.SaveChangesAsync();
            return _mapper.Map<OrganizationDTO>(newOrganization);
        }

        public async Task UpdateOrganizationAsync(OrganizationDTO updatedOrganizationDto)
        {
            var existingOrganization = await _context.Organizations.FindAsync(updatedOrganizationDto.OrganizationID);

            if (existingOrganization != null)
            {
                _mapper.Map(updatedOrganizationDto, existingOrganization);
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
