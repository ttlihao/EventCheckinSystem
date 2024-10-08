using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCheckinSystem.Services.Services
{
    public class GuestGroupServices : IGuestGroupServices
    {
        private readonly EventCheckinManagementContext _context;

        public GuestGroupServices(EventCheckinManagementContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GuestGroup>> GetAllGuestGroupsAsync()
        {
            return await _context.GuestGroups
                                 .Include(g => g.Organization)
                                 .Include(g => g.Event)
                                 .Include(g => g.Guests)
                                 .ToListAsync();
        }

        public async Task<GuestGroup> GetGuestGroupByIdAsync(int id)
        {
            return await _context.GuestGroups
                                 .Include(g => g.Organization)
                                 .Include(g => g.Event)
                                 .Include(g => g.Guests)
                                 .FirstOrDefaultAsync(g => g.GuestGroupID == id);
        }

        public async Task<GuestGroup> CreateGuestGroupAsync(GuestGroup guestGroup)
        {
            await _context.GuestGroups.AddAsync(guestGroup);
            await _context.SaveChangesAsync();
            return guestGroup;
        }

        public async Task UpdateGuestGroupAsync(GuestGroup updatedGroup)
        {
            var existingGroup = await _context.GuestGroups.FindAsync(updatedGroup.GuestGroupID);

            if (existingGroup != null)
            {
                existingGroup.Name = updatedGroup.Name;
                existingGroup.OrganizationID = updatedGroup.OrganizationID;
                existingGroup.EventID = updatedGroup.EventID;
                existingGroup.Guests = updatedGroup.Guests;

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

        public async Task<GuestGroup> GetGuestGroupByGuestIdAsync(int guestId)
        {
            return await _context.GuestGroups
                                 .Include(g => g.Organization)
                                 .Include(g => g.Event)
                                 .Include(g => g.Guests)
                                 .FirstOrDefaultAsync(g => g.Guests.Any(guest => guest.GuestID == guestId));
        }
    }
}
