using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCheckinSystem.Services.Services
{
    public class EventServices : IEventServices
    {
        private readonly EventCheckinManagementContext _context;

        public EventServices(EventCheckinManagementContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            return await _context.Events
                                 .Include(e => e.Organization)
                                 .Include(e => e.GuestGroups)
                                 .Include(e => e.UserEvents)
                                 .ToListAsync();
        }

        public async Task<Event> GetEventByIdAsync(int id)
        {
            return await _context.Events
                                 .Include(e => e.Organization)
                                 .Include(e => e.GuestGroups)
                                 .Include(e => e.UserEvents)
                                 .FirstOrDefaultAsync(e => e.EventID == id);
        }

        public async Task<Event> CreateEventAsync(Event newEvent)
        {
            await _context.Events.AddAsync(newEvent);
            await _context.SaveChangesAsync();
            return newEvent;
        }

        public async Task UpdateEventAsync(Event updatedEvent)
        {
            var existingEvent = await _context.Events.FindAsync(updatedEvent.EventID);

            if (existingEvent != null)
            {
                existingEvent.Name = updatedEvent.Name;
                existingEvent.OrganizationID = updatedEvent.OrganizationID;
                existingEvent.GuestGroups = updatedEvent.GuestGroups;
                existingEvent.UserEvents = updatedEvent.UserEvents;

                _context.Events.Update(existingEvent);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteEventAsync(int id)
        {
            var eventToDelete = await _context.Events.FindAsync(id);
            if (eventToDelete != null)
            {
                _context.Events.Remove(eventToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
