using AutoMapper;
using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Repo.DTOs.Paging;
using EventCheckinSystem.Repo.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventCheckinSystem.Repo.Repositories.Implements
{
    public class UserEventRepo : IUserEventRepo
    {
        private readonly EventCheckinManagementContext _context;

        public UserEventRepo(EventCheckinManagementContext context)
        {
            _context = context;
        }

        public async Task<List<UserEvent>> GetAllUserEventsAsync()
        {
            var userEvents = await _context.UserEvents
                .Include(ue => ue.Event)
                .ToListAsync();

            return userEvents;
        }

        public async Task<UserEvent> GetUserEventByIdAsync(int eventId)
        {
            var userEvent = await _context.UserEvents
                .Include(ue => ue.Event)
                .FirstOrDefaultAsync(ue => ue.EventID == eventId);

            return userEvent;
        }

        public async Task<UserEvent> AddUserEventAsync(UserEvent userEvent)
        {
            await _context.UserEvents.AddAsync(userEvent);
            await _context.SaveChangesAsync();
            return userEvent;
        }

        public async Task<bool> UpdateUserEventAsync(UserEvent userEvent)
        {
            bool isSuccess = false;
            try
            {
                var existingUserEvent = await _context.UserEvents.FirstOrDefaultAsync(e => e.Id == userEvent.Id);
                if (existingUserEvent != null)
                {
                    _context.Entry(existingUserEvent).State = EntityState.Detached; // Detach the existing entity
                    _context.UserEvents.Attach(userEvent);
                    _context.Entry(userEvent).State = EntityState.Modified; // Mark as modified
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

        public async Task<bool> DeleteUserEventAsync(int eventId)
        {
            var userEvent = await _context.UserEvents.FindAsync(eventId);
            if (userEvent != null)
            {
                _context.UserEvents.Remove(userEvent);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<PagedResult<UserEvent>> GetPagedUserEventsAsync(PageRequest pageRequest)
        {
            var query = _context.UserEvents
                .Include(ue => ue.Event);

            return await query.CreatePagingAsync(pageRequest.PageNumber, pageRequest.PageSize);
        }

        public async Task<List<UserEvent>> GetUserEventsByUserIdAsync(string userId)
        {
            var userEvents = await _context.UserEvents
                .Include(ue => ue.Event)
                .Where(ue => ue.UserID == userId)
                .ToListAsync();
            return userEvents;
        }

    }
}
