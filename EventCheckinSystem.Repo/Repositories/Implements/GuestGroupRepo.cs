using AutoMapper;
using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Repo.DTOs;
using EventCheckinSystem.Repo.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCheckinSystem.Repo.Repositories.Implements
{
    public class GuestGroupRepo : IGuestGroupRepo
    {
        private readonly EventCheckinManagementContext _context;
        private readonly IMapper _mapper;

        public GuestGroupRepo(EventCheckinManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GuestGroupDTO>> GetAllGuestGroupsAsync()
        {
            var guestGroups = await _context.GuestGroups
                                             .Include(g => g.Organization)
                                             .Include(g => g.Event)
                                             .Include(g => g.Guests)
                                             .ToListAsync();

            return _mapper.Map<IEnumerable<GuestGroupDTO>>(guestGroups);
        }

        public async Task<GuestGroupDTO> GetGuestGroupByIdAsync(int id)
        {
            var guestGroup = await _context.GuestGroups
                                            .Include(g => g.Organization)
                                            .Include(g => g.Event)
                                            .Include(g => g.Guests)
                                            .FirstOrDefaultAsync(g => g.GuestGroupID == id);

            return guestGroup == null ? null : _mapper.Map<GuestGroupDTO>(guestGroup);
        }

        public async Task<GuestGroupDTO> CreateGuestGroupAsync(GuestGroupDTO guestGroupDto, string createdBy)
        {
            var guestGroup = _mapper.Map<GuestGroup>(guestGroupDto);
            guestGroup.CreatedBy = createdBy; // Set the createdBy
            guestGroup.LastUpdatedBy = createdBy; // Set lastUpdatedBy

            await _context.GuestGroups.AddAsync(guestGroup);
            await _context.SaveChangesAsync();
            return _mapper.Map<GuestGroupDTO>(guestGroup);
        }

        public async Task UpdateGuestGroupAsync(GuestGroupDTO guestGroupDto, string updatedBy)
        {
            var existingGroup = await _context.GuestGroups.FindAsync(guestGroupDto.GuestGroupID);
            if (existingGroup != null)
            {
                _mapper.Map(guestGroupDto, existingGroup); // Map properties from DTO to existing entity
                existingGroup.LastUpdatedBy = updatedBy; // Update lastUpdatedBy

                _context.GuestGroups.Update(existingGroup);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteGuestGroupAsync(int id)
        {
            var groupToDelete = await _context.GuestGroups.FindAsync(id);
            if (groupToDelete != null)
            {
                _context.GuestGroups.Remove(groupToDelete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<GuestGroupDTO> GetGuestGroupByGuestIdAsync(int guestId)
        {
            var guestGroup = await _context.GuestGroups
                                            .Include(g => g.Organization)
                                            .Include(g => g.Event)
                                            .Include(g => g.Guests)
                                            .FirstOrDefaultAsync(g => g.Guests.Any(guest => guest.GuestID == guestId));

            return guestGroup == null ? null : _mapper.Map<GuestGroupDTO>(guestGroup);
        }
    }
}
