using AutoMapper;
using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Repo.DTOs;
using EventCheckinSystem.Repo.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCheckinSystem.Repo.Repositories.Implements
{
    public class GuestCheckinRepo : IGuestCheckinRepo
    {
        private readonly EventCheckinManagementContext _context;
        private readonly IMapper _mapper;

        public GuestCheckinRepo(EventCheckinManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GuestCheckinDTO>> GetAllCheckinsAsync()
        {
            var checkins = await _context.GuestCheckins
                                          .Include(gc => gc.Guest)
                                          .ToListAsync();
            return _mapper.Map<IEnumerable<GuestCheckinDTO>>(checkins);
        }

        public async Task<GuestCheckinDTO> GetCheckinByIdAsync(int id)
        {
            var guestCheckin = await _context.GuestCheckins
                                              .Include(gc => gc.Guest)
                                              .FirstOrDefaultAsync(gc => gc.GuestCheckinID == id);
            return guestCheckin == null ? null : _mapper.Map<GuestCheckinDTO>(guestCheckin);
        }

        public async Task<GuestCheckinDTO> CreateCheckinAsync(GuestCheckinDTO guestCheckinDto, string createdBy)
        {
            var newCheckin = _mapper.Map<GuestCheckin>(guestCheckinDto);
            newCheckin.CreatedBy = createdBy;
            newCheckin.LastUpdatedBy = createdBy;

            await _context.GuestCheckins.AddAsync(newCheckin);
            await _context.SaveChangesAsync();

            return _mapper.Map<GuestCheckinDTO>(newCheckin);
        }

        public async Task UpdateCheckinAsync(GuestCheckinDTO updatedCheckinDto)
        {
            var existingCheckin = await _context.GuestCheckins.FindAsync(updatedCheckinDto.GuestCheckinID);
            if (existingCheckin != null)
            {
                _mapper.Map(updatedCheckinDto, existingCheckin);
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

        public async Task<GuestCheckinDTO> CheckinGuestByIdAsync(int guestId, string createdBy)
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

            return _mapper.Map<GuestCheckinDTO>(checkin);
        }
    }
}
