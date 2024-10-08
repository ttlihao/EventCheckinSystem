using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCheckinSystem.Services.Services
{
    public class UserEventServices : IUserEventServices
    {
        private readonly EventCheckinManagementContext _context;

        public UserEventServices(EventCheckinManagementContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserEvent>> GetAllUserEventsAsync()
        {
            return await _context.UserEvents
                                 .Include(ue => ue.User)
                                 .Include(ue => ue.Event)
                                 .ToListAsync();
        }

        public async Task<UserEvent> GetUserEventAsync(string userId, int eventId)
        {
            return await _context.UserEvents
                                 .Include(ue => ue.User)
                                 .Include(ue => ue.Event)
                                 .FirstOrDefaultAsync(ue => ue.UserID == userId && ue.EventID == eventId);
        }

        public async Task<UserEvent> CreateUserEventAsync(UserEvent userEvent)
        {
            await _context.UserEvents.AddAsync(userEvent);
            await _context.SaveChangesAsync();
            return userEvent;
        }

        public async Task DeleteUserEventAsync(string userId, int eventId)
        {
            var userEvent = await _context.UserEvents.FirstOrDefaultAsync(ue => ue.UserID == userId && ue.EventID == eventId);
            if (userEvent != null)
            {
                _context.UserEvents.Remove(userEvent);
                await _context.SaveChangesAsync();
            }
        }
    }
}
