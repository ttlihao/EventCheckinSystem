using AutoMapper;
using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Repo.DTOs;
using EventCheckinSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventCheckinSystem.Services
{
    public class GuestServices : IGuestServices
    {
        private readonly EventCheckinManagementContext _context;
        private readonly IMapper _mapper; // Add a mapper instance

        public GuestServices(EventCheckinManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper; // Initialize the mapper
        }

        public async Task<List<GuestDTO>> GetAllGuestsAsync()
        {
            var guests = await _context.Set<Guest>()
                                       .Include(g => g.GuestGroup)
                                       .Include(g => g.GuestImage)
                                       .Include(g => g.GuestCheckin)
                                       .ToListAsync();

            // Use AutoMapper to map the list of guests to GuestDTOs
            return _mapper.Map<List<GuestDTO>>(guests);
        }

        public async Task<GuestDTO> GetGuestByIdAsync(int guestId)
        {
            var guest = await _context.Set<Guest>()
                                      .Include(g => g.GuestGroup)
                                      .Include(g => g.GuestImage)
                                      .Include(g => g.GuestCheckin)
                                      .FirstOrDefaultAsync(g => g.GuestID == guestId);

            // If guest is not found, return null
            if (guest == null) return null;

            // Use AutoMapper to map the guest to GuestDTO
            return _mapper.Map<GuestDTO>(guest);
        }

        public async Task AddGuestAsync(Guest guest, string createdBy)
        {
            guest.CreatedBy = createdBy;
            await _context.Set<Guest>().AddAsync(guest);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateGuestAsync(Guest guest, string updatedBy)
        {
            guest.LastUpdatedBy = updatedBy;

            // Only attach if the entity is not already tracked
            var entry = _context.Entry(guest);
            if (entry.State == EntityState.Detached)
            {
                _context.Set<Guest>().Attach(guest);
            }
            entry.State = EntityState.Modified; // Mark as modified
            await _context.SaveChangesAsync();
        }

        public async Task DeleteGuestAsync(int guestId)
        {
            var guest = await _context.Set<Guest>().FindAsync(guestId);
            if (guest != null)
            {
                _context.Set<Guest>().Remove(guest);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<GuestDTO>> GetGuestsByGroupIdAsync(int guestGroupId)
        {
            var guests = await _context.Set<Guest>()
                                       .Include(g => g.GuestGroup)
                                       .Include(g => g.GuestImage)
                                       .Include(g => g.GuestCheckin)
                                       .Where(g => g.GuestGroupID == guestGroupId)
                                       .ToListAsync();

            // Use AutoMapper to map the list of guests to GuestDTOs
            return _mapper.Map<List<GuestDTO>>(guests);
        }
    }
}
