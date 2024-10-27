using AutoMapper;
using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Repo.DTOs;
using EventCheckinSystem.Repo.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCheckinSystem.Repo.Repositories.Implements
{
    public class GuestImageRepo : IGuestImageRepo
    {
        private readonly EventCheckinManagementContext _context;
        private readonly IMapper _mapper;

        public GuestImageRepo(EventCheckinManagementContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GuestImageDTO>> GetAllGuestImagesAsync()
        {
            var guestImages = await _context.GuestImages
                .Include(g => g.Guest)
                .ToListAsync();

            return _mapper.Map<IEnumerable<GuestImageDTO>>(guestImages);
        }

        public async Task<GuestImageDTO> GetGuestImageByIdAsync(int id)
        {
            var guestImage = await _context.GuestImages
                .Include(g => g.Guest)
                .FirstOrDefaultAsync(g => g.GuestImageID == id);

            return _mapper.Map<GuestImageDTO>(guestImage);
        }

        public async Task<GuestImageDTO> CreateGuestImageAsync(GuestImageDTO guestImageDto)
        {
            var guestImage = _mapper.Map<GuestImage>(guestImageDto);
            await _context.GuestImages.AddAsync(guestImage);
            await _context.SaveChangesAsync();
            return _mapper.Map<GuestImageDTO>(guestImage);
        }

        public async Task UpdateGuestImageAsync(GuestImageDTO guestImageDto)
        {
            var existingImage = await _context.GuestImages.FindAsync(guestImageDto.GuestImageID);

            if (existingImage != null)
            {
                _mapper.Map(guestImageDto, existingImage); // Mapping DTO properties to existing entity
                _context.GuestImages.Update(existingImage);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteGuestImageAsync(int id)
        {
            var imageToDelete = await _context.GuestImages.FindAsync(id);
            if (imageToDelete != null)
            {
                _context.GuestImages.Remove(imageToDelete);
                await _context.SaveChangesAsync();
            }
        }
    }
}
