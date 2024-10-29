using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Repo.DTOs;
using EventCheckinSystem.Repo.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventCheckinSystem.Repo.Repositories.Implements
{
    public class GuestRepo : IGuestRepo
    {
        private readonly EventCheckinManagementContext _context;

        public GuestRepo(EventCheckinManagementContext context)
        {
            _context = context;
        }

        public async Task<List<Guest>> GetAllGuestsAsync()
        {
            try
            {
                var guests = await _context.Set<Guest>()
                    .Where(e => e.IsActive && !e.IsDelete)
                    .Include(g => g.GuestGroup)
                    .Include(g => g.GuestImage)
                    .Include(g => g.GuestCheckin)
                    .ToListAsync();

                return guests;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving all Guests: {ex.Message}");
            }
        }

        public async Task<Guest> GetGuestByIdAsync(int guestId)
        {
            try
            {
                var guest = await _context.Set<Guest>()
                    .Where(e => e.IsActive && !e.IsDelete)
                    .Include(g => g.GuestGroup)
                    .Include(g => g.GuestImage)
                    .Include(g => g.GuestCheckin)
                    .FirstOrDefaultAsync(g => g.GuestID == guestId);

                if (guest == null)
                {
                    throw new NullReferenceException($"Guest with ID {guestId} not found.");
                }

                return guest;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving Guest with ID {guestId}: {ex.Message}");
            }
        }

        public async Task<Guest> AddGuestAsync(Guest guest)
        {
            try
            {
                await _context.Set<Guest>().AddAsync(guest);
                await _context.SaveChangesAsync();
                return guest;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding Guest: {ex.Message}");
            }
        }

        public async Task<bool> UpdateGuestAsync(Guest guest)
        {
            bool isSuccess = false;
            try
            {
                var existingGuest = await _context.Guests.FirstOrDefaultAsync(e => e.GuestID == guest.GuestID);
                if (existingGuest != null)
                {
                    _context.Entry(existingGuest).State = EntityState.Detached; // Detach the existing entity
                    _context.Guests.Attach(guest);
                    _context.Entry(guest).State = EntityState.Modified; // Mark as modified
                    var changes = await _context.SaveChangesAsync();
                    isSuccess = changes > 0; // Return true if changes were made
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return isSuccess;
        }

        public async Task<bool> DeleteGuestAsync(int guestId)
        {
            try
            {
                var guest = await _context.Set<Guest>().FindAsync(guestId);
                if (guest != null)
                {
                    _context.Set<Guest>().Remove(guest);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    throw new NullReferenceException($"Guest with ID {guestId} not found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting Guest with ID {guestId}: {ex.Message}");
            }
        }

        public async Task<List<Guest>> GetGuestsByGroupIdAsync(int guestGroupId)
        {
            try
            {
                var guests = await _context.Set<Guest>()
                    .Where(e => e.IsActive && !e.IsDelete)
                    .Include(g => g.GuestGroup)
                    .Include(g => g.GuestImage)
                    .Include(g => g.GuestCheckin)
                    .Where(g => g.GuestGroupID == guestGroupId)
                    .ToListAsync();

                return guests;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving Guests by Group ID {guestGroupId}: {ex.Message}");
            }
        }
    }
}
