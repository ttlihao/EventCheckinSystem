using AutoMapper;
using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Repo.DTOs;
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
        private readonly IMapper _mapper;

        public EventServices(EventCheckinManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EventDTO>> GetAllEventsAsync()
        {
            var events = await _context.Events
                .Include(e => e.Organization)
                .Include(e => e.GuestGroups)
                .Include(e => e.UserEvents)
                .ToListAsync();

            return _mapper.Map<IEnumerable<EventDTO>>(events);
        }

        public async Task<EventDTO> GetEventByIdAsync(int id)
        {
            var eventEntity = await _context.Events
                .Include(e => e.Organization)
                .Include(e => e.GuestGroups)
                .Include(e => e.UserEvents)
                .FirstOrDefaultAsync(e => e.EventID == id);

            return _mapper.Map<EventDTO>(eventEntity);
        }

        public async Task<EventDTO> CreateEventAsync(EventDTO newEventDto)
        {
            var newEvent = _mapper.Map<Event>(newEventDto);
            await _context.Events.AddAsync(newEvent);
            await _context.SaveChangesAsync();
            return _mapper.Map<EventDTO>(newEvent);
        }

        public async Task UpdateEventAsync(EventDTO updatedEventDto)
        {
            var existingEvent = await _context.Events.FindAsync(updatedEventDto.EventID);

            if (existingEvent != null)
            {
                _mapper.Map(updatedEventDto, existingEvent);
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
