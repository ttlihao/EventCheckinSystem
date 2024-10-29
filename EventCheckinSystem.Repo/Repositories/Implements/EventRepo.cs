using AutoMapper;
using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Repo.DTOs;
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
        private readonly IMapper _mapper;

        public EventRepo(EventCheckinManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Event>> GetAllEventsAsync()
        {
            var events = await _context.Events
                .Where(e => e.IsActive && !e.IsDelete)
                .Include(e => e.Organization)
                .Include(e => e.GuestGroups)
                .Include(e => e.UserEvents)
                .ToListAsync();

            return _mapper.Map<IEnumerable<Event>>(events);
        }

        public async Task<Event> GetEventByIdAsync(int id)
        {
            var eventEntity = await _context.Events
                .Where(e => e.IsActive && !e.IsDelete)
                .Include(e => e.Organization)
                .Include(e => e.GuestGroups)
                .Include(e => e.UserEvents)
                .FirstOrDefaultAsync(e => e.EventID == id);

            return _mapper.Map<Event>(eventEntity);
        }

        public async Task<Event> CreateEventAsync(Event newEventDto)
        {
            var newEvent = _mapper.Map<Event>(newEventDto);
            await _context.Events.AddAsync(newEvent);
            await _context.SaveChangesAsync();
            return _mapper.Map<Event>(newEvent);
        }

        public async Task UpdateEventAsync(Event updatedEvent)
        {
            var existingEvent = await _context.Events.FindAsync(updatedEvent.EventID);

            if (existingEvent != null)
            {
                _mapper.Map(updatedEvent, existingEvent);
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
