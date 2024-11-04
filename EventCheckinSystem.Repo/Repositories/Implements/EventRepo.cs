using AutoMapper;
using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Repo.DTOs;
using EventCheckinSystem.Repo.DTOs.Paging;
using EventCheckinSystem.Repo.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCheckinSystem.Repo.Repositories.Implements
{
    public class EventRepo : IEventRepo
    {
        private readonly EventCheckinManagementContext _context;

        public EventRepo(EventCheckinManagementContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            var events = await _context.Events
                .Where(e => e.IsActive && !e.IsDelete)
                .Include(e => e.Organization)
                .Include(e => e.GuestGroups)
                .Include(e => e.UserEvents)
                .ToListAsync();

            return events;
        }

        public async Task<Event> GetEventByIdAsync(int id)
        {
            var eventEntity = await _context.Events
                .Where(e => e.IsActive && !e.IsDelete)
                .Include(e => e.Organization)
                .Include(e => e.GuestGroups)
                .Include(e => e.UserEvents)
                .FirstOrDefaultAsync(e => e.EventID == id);

            return eventEntity;
        }

        public async Task<Event> CreateEventAsync(Event newEventDto)
        {
            var newEvent = newEventDto;
            await _context.Events.AddAsync(newEvent);
            await _context.SaveChangesAsync();
            return newEvent;
        }
        public async Task<bool> UpdateEventAsync(Event updatedEvent)
        {
            bool isSuccess = false;
            try
            {
                var existingEvent = await _context.Events.FirstOrDefaultAsync(e => e.EventID == updatedEvent.EventID);
                if (existingEvent != null)
                {
                    _context.Entry(existingEvent).State = EntityState.Detached; // Detach the existing entity
                    _context.Events.Attach(updatedEvent);
                    _context.Entry(updatedEvent).State = EntityState.Modified; // Mark as modified
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



        public async Task<bool> DeleteEventAsync(int id)
        {
            var eventToDelete = await _context.Events.FindAsync(id);
            if (eventToDelete != null)
            {
                eventToDelete.IsActive = false;
                eventToDelete.IsDelete = true;
                _context.Events.Update(eventToDelete);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<PagedResult<Event>> GetPagedEventsAsync(PageRequest pageRequest)
        {
            var query = _context.Events
                .Where(e => e.IsActive && !e.IsDelete)
                .Include(e => e.Organization)
                .Include(e => e.GuestGroups)
                .Include(e => e.UserEvents);

            return await query.CreatePagingAsync(pageRequest.PageNumber, pageRequest.PageSize);
        }
    }
}
