using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCheckinSystem.Services
{
    public class GuestServices : IGuestServices
    {
        private readonly EventCheckinManagementContext _context;

        public GuestServices(EventCheckinManagementContext context)
        {
            _context = context;
        }

        public async Task<List<Guest>> GetAllGuestsAsync()
        {
            return await _context.Set<Guest>()
                                 .Include(g => g.GuestGroup)
                                 .Include(g => g.GuestImage)
                                 .Include(g => g.GuestCheckin)
                                 .ToListAsync();
        }

        public async Task<Guest> GetGuestByIdAsync(int guestId)
        {
            return await _context.Set<Guest>()
                                 .Include(g => g.GuestGroup)
                                 .Include(g => g.GuestImage)
                                 .Include(g => g.GuestCheckin)
                                 .FirstOrDefaultAsync(g => g.GuestID == guestId);
        }

        public async Task AddGuestAsync(Guest guest)
        {
            _context.Set<Guest>().Add(guest);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGuestAsync(Guest guest)
        {
            _context.Set<Guest>().Update(guest);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGuestAsync(int guestId)
        {
            var guest = await _context.Set<Guest>().FindAsync(guestId);
            if (guest != null)
            {
                _context.Set<Guest>().Remove(guest);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Guest>> GetGuestsByGroupIdAsync(int guestGroupId)
        {
            try
            {
                return await _context.Set<Guest>()
                                     .Include(g => g.GuestGroup)
                                     .Include(g => g.GuestImage)
                                     .Include(g => g.GuestCheckin)
                                     .Where(g => g.GuestGroupID == guestGroupId)
                                     .ToListAsync();
            }
            catch (Exception ex)
            {              
                throw new Exception($"An error occurred while retrieving guests for group ID {guestGroupId}.", ex);
            }
        }
    }
}
