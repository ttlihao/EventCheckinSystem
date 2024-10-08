using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCheckinSystem.Services.Services
{
    public class GuestImageServices : IGuestImageServices
    {
        private readonly EventCheckinManagementContext _context;

        public GuestImageServices(EventCheckinManagementContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GuestImage>> GetAllGuestImagesAsync()
        {
            return await _context.GuestImages
                                 .Include(g => g.Guest)
                                 .ToListAsync();
        }

        public async Task<GuestImage> GetGuestImageByIdAsync(int id)
        {
            return await _context.GuestImages
                                 .Include(g => g.Guest)
                                 .FirstOrDefaultAsync(g => g.GuestImageID == id);
        }

        public async Task<GuestImage> CreateGuestImageAsync(GuestImage guestImage)
        {
            await _context.GuestImages.AddAsync(guestImage);
            await _context.SaveChangesAsync();
            return guestImage;
        }

        public async Task UpdateGuestImageAsync(GuestImage updatedImage)
        {
            var existingImage = await _context.GuestImages.FindAsync(updatedImage.GuestImageID);

            if (existingImage != null)
            {
                existingImage.GuestID = updatedImage.GuestID;
                existingImage.ImageURL = updatedImage.ImageURL;

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
