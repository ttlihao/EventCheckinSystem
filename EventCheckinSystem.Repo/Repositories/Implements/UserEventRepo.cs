using AutoMapper;
using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Repo.DTOs;
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
        private readonly IMapper _mapper;

        public UserEventRepo(EventCheckinManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<UserEventDTO>> GetAllUserEventsAsync()
        {
            var userEvents = await _context.UserEvents
                .Include(ue => ue.Event)
                .ToListAsync();

            return _mapper.Map<List<UserEventDTO>>(userEvents);
        }

        public async Task<UserEventDTO> GetUserEventByIdAsync(int eventId)
        {
            var userEvent = await _context.UserEvents
                .Include(ue => ue.Event)
                .FirstOrDefaultAsync(ue => ue.EventID == eventId);

            return _mapper.Map<UserEventDTO>(userEvent);
        }

        public async Task AddUserEventAsync(UserEventDTO userEventDto)
        {
            var userEvent = _mapper.Map<UserEvent>(userEventDto);
            await _context.UserEvents.AddAsync(userEvent);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserEventAsync(UserEventDTO userEventDto)
        {
            var userEvent = await _context.UserEvents.FindAsync(userEventDto.EventID);
            if (userEvent != null)
            {
                _mapper.Map(userEventDto, userEvent);
                _context.UserEvents.Update(userEvent);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteUserEventAsync(int eventId)
        {
            var userEvent = await _context.UserEvents.FindAsync(eventId);
            if (userEvent != null)
            {
                _context.UserEvents.Remove(userEvent);
                await _context.SaveChangesAsync();
            }
        }
    }
}
