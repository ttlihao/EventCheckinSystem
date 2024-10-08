using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCheckinSystem.Services.Services
{
    public class GuestCheckinServices : IGuestCheckinServices
    {
        private readonly EventCheckinManagementContext _context;

        public GuestCheckinServices(EventCheckinManagementContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GuestCheckin>> GetAllCheckinsAsync()
        {
            return await _context.GuestCheckins
                                 .Include(gc => gc.Guest)
                                 .ToListAsync();
        }

        public async Task<GuestCheckin> GetCheckinByIdAsync(int id)
        {
            return await _context.GuestCheckins
                                 .Include(gc => gc.Guest)
                                 .FirstOrDefaultAsync(gc => gc.GuestCheckinID == id);
        }

        public async Task<GuestCheckin> CreateCheckinAsync(GuestCheckin guestCheckin)
        {
            await _context.GuestCheckins.AddAsync(guestCheckin);
            await _context.SaveChangesAsync();
            return guestCheckin;
        }

        public async Task UpdateCheckinAsync(GuestCheckin updatedCheckin)
        {
            var existingCheckin = await _context.GuestCheckins.FindAsync(updatedCheckin.GuestCheckinID);

            if (existingCheckin != null)
            {
                existingCheckin.CheckinTime = updatedCheckin.CheckinTime;
                existingCheckin.GuestID = updatedCheckin.GuestID;

                _context.GuestCheckins.Update(existingCheckin);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteCheckinAsync(int id)
        {
            var checkinToDelete = await _context.GuestCheckins.FindAsync(id);
            if (checkinToDelete != null)
            {
                _context.GuestCheckins.Remove(checkinToDelete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<GuestCheckin> CheckinGuestByIdAsync(int guestId, string createdBy)
        {
            var checkin = new GuestCheckin
            {
                GuestID = guestId,
                CheckinTime = DateTime.UtcNow,
                CreatedBy = createdBy,
                LastUpdatedBy = createdBy   
            };

            await _context.GuestCheckins.AddAsync(checkin);
            await _context.SaveChangesAsync();

            return checkin;
        }


    }
}
