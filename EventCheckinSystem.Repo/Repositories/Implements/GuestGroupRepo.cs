using EventCheckinSystem.Repo.Data;
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
                // Log the exception
                // Logger.LogError(ex.Message);
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

        public async Task UpdateGuestGroupAsync(GuestGroup guestGroup, string updatedBy)
        {
            try
            {
                var existingGroup = await _context.GuestGroups.FindAsync(guestGroup.GuestGroupID);
                if (existingGroup != null)
                {
                    existingGroup.Name = guestGroup.Name;
                    existingGroup.OrganizationID = guestGroup.OrganizationID;
                    existingGroup.EventID = guestGroup.EventID;
                    existingGroup.IsActive = guestGroup.IsActive;
                    existingGroup.IsDelete = guestGroup.IsDelete;
                    existingGroup.LastUpdatedBy = updatedBy; // Update lastUpdatedBy
                    _context.GuestGroups.Update(existingGroup);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new NullReferenceException($"GuestGroup with ID {guestGroup.GuestGroupID} not found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating GuestGroup: {ex.Message}");
            }
        }


        public async Task DeleteGuestGroupAsync(int id)
        {
            try
            {
                var groupToDelete = await _context.GuestGroups.FindAsync(id);
                if (groupToDelete != null)
                {
                    _context.GuestGroups.Remove(groupToDelete);
                    await _context.SaveChangesAsync();
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
    }
}
