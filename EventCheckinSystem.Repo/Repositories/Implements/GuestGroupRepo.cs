using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Repo.DTOs.Paging;
using EventCheckinSystem.Repo.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventCheckinSystem.Repo.Repositories.Implements
{
    public class GuestGroupRepo : IGuestGroupRepo
    {
        private readonly EventCheckinManagementContext _context;

        public GuestGroupRepo(EventCheckinManagementContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GuestGroup>> GetAllGuestGroupsAsync()
        {
            try
            {
                return await _context.GuestGroups
                    .Where(g => g.IsActive && !g.IsDelete)
                    .Include(g => g.Organization)
                    .Include(g => g.Event)
                    .Include(g => g.Guests)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving all GuestGroups: {ex.Message}");
            }
        }


        public async Task<GuestGroup> GetGuestGroupByIdAsync(int id)
        {
            try
            {
                var guestGroup = await _context.GuestGroups
                    .Where(g => g.IsActive && !g.IsDelete)
                    .Include(g => g.Organization)
                    .Include(g => g.Event)
                    .Include(g => g.Guests)
                    .FirstOrDefaultAsync(g => g.GuestGroupID == id);

                if (guestGroup == null)
                {
                    throw new NullReferenceException($"GuestGroup with ID {id} not found.");
                }

                return guestGroup;
            }
            catch (Exception ex)
            {
                // Log the exception
                // Logger.LogError(ex.Message);
                throw new Exception($"Error retrieving GuestGroup with ID {id}: {ex.Message}");
            }
        }


        public async Task<GuestGroup> CreateGuestGroupAsync(GuestGroup guestGroup)
        {
            await _context.GuestGroups.AddAsync(guestGroup);
            await _context.SaveChangesAsync();
            return guestGroup;
        }

        public async Task<bool> UpdateGuestGroupAsync(GuestGroup guestGroup)
        {
            bool isSuccess = false;
            try
            {
                var existingGuestGroup = await _context.GuestGroups.FirstOrDefaultAsync(e => e.GuestGroupID == guestGroup.GuestGroupID);
                if (existingGuestGroup != null)
                {
                    _context.Entry(existingGuestGroup).State = EntityState.Detached; // Detach the existing entity
                    _context.GuestGroups.Attach(guestGroup);
                    _context.Entry(guestGroup).State = EntityState.Modified; // Mark as modified
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


        public async Task<bool> DeleteGuestGroupAsync(int id)
        {
            try
            {
                var groupToDelete = await _context.GuestGroups.FindAsync(id);
                if (groupToDelete != null)
                {
                    groupToDelete.IsActive = false;
                    groupToDelete.IsDelete = true;
                    _context.GuestGroups.Update(groupToDelete);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    throw new NullReferenceException($"GuestGroup with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                // Logger.LogError(ex.Message);
                throw new Exception($"Error deleting GuestGroup with ID {id}: {ex.Message}");
            }
        }

        public async Task<GuestGroup> GetGuestGroupByGuestIdAsync(int guestId)
        {
            try
            {
                var guestGroup = await _context.GuestGroups
                    .Where(g => g.IsActive && !g.IsDelete)
                    .Include(g => g.Organization)
                    .Include(g => g.Event)
                    .Include(g => g.Guests)
                    .FirstOrDefaultAsync(g => g.Guests.Any(guest => guest.GuestID == guestId));
                if (guestGroup == null)
                {
                    throw new NullReferenceException($"GuestGroup with Guest ID {guestId} not found.");
                }
                return guestGroup;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving GuestGroup with guestID {guestId}: {ex.Message}");
            }
        }

        public async Task<PagedResult<GuestGroup>> GetPagedGuestGroupsAsync(PageRequest pageRequest)
        {
            var query = _context.GuestGroups
                .Where(g => g.IsActive && !g.IsDelete)
                .Include(g => g.Organization)
                .Include(g => g.Event)
                .Include(g => g.Guests);
            return await query.CreatePagingAsync(pageRequest.PageNumber, pageRequest.PageSize);
        }

        public async Task<List<GuestGroup>> GetGuestGroupsByUserIdAsync(string userId)
        {
            var eventIds = await _context.UserEvents
                .Where(ue => ue.UserID == userId)
                .Select(ue => ue.EventID)
                .ToListAsync();
            var guestGroups = await _context.GuestGroups
                .Where(gg => eventIds.Contains(gg.EventID))
                .Include(g => g.Organization)
                .Include(g => g.Event)
                .Include(g => g.Guests)
                .ToListAsync();
            return guestGroups;
        }

        public async Task<List<GuestGroup>> GetGuestGroupsByEventIdAsync(int eventId)
        {
            return await _context.GuestGroups
                .Where(gg => gg.EventID == eventId)
                .Include(g => g.Organization)
                .Include(g => g.Event)
                .Include(g => g.Guests)
                .ToListAsync();
        }
    }
}
