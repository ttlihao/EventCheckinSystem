using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Repo.DTOs;
using EventCheckinSystem.Repo.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventCheckinSystem.Repo.Repositories.Implements
{
    public class GuestImageRepo : IGuestImageRepo
    {
        private readonly EventCheckinManagementContext _context;

        public GuestImageRepo(EventCheckinManagementContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GuestImage>> GetAllGuestImagesAsync()
        {
            try
            {
                var guestImages = await _context.GuestImages
                    .Where(e => e.IsActive && !e.IsDelete)
                    .Include(g => g.Guest)
                    .ToListAsync();

                return guestImages;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving all GuestImages: {ex.Message}");
            }
        }

        public async Task<GuestImage> GetGuestImageByIdAsync(int id)
        {
            try
            {
                var guestImage = await _context.GuestImages
                    .Where(e => e.IsActive && !e.IsDelete)
                    .Include(g => g.Guest)
                    .FirstOrDefaultAsync(g => g.GuestImageID == id);

                if (guestImage == null)
                {
                    throw new NullReferenceException($"GuestImage with ID {id} not found.");
                }

                return guestImage;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving GuestImage with ID {id}: {ex.Message}");
            }
        }

        public async Task<GuestImage> CreateGuestImageAsync(GuestImage guestImage)
        {
            try
            {
                await _context.GuestImages.AddAsync(guestImage);
                await _context.SaveChangesAsync();

                guestImage.GuestImageID = guestImage.GuestImageID;
                return guestImage;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating GuestImage: {ex.Message}");
            }
        }

        public async Task<bool> UpdateGuestImageAsync(GuestImage guestImage)
        {
            bool isSuccess = false;
            try
            {
                var existingGuestImage = await _context.GuestImages.FirstOrDefaultAsync(e => e.GuestImageID == guestImage.GuestImageID);
                if (existingGuestImage != null)
                {
                    _context.Entry(existingGuestImage).State = EntityState.Detached; // Detach the existing entity
                    _context.GuestImages.Attach(guestImage);
                    _context.Entry(guestImage).State = EntityState.Modified; // Mark as modified
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

        public async Task<bool> DeleteGuestImageAsync(int id)
        {
            try
            {
                var imageToDelete = await _context.GuestImages.FindAsync(id);
                if (imageToDelete != null)
                {
                    imageToDelete.IsActive = false;
                    imageToDelete.IsDelete = true;
                    _context.GuestImages.Update(imageToDelete);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    throw new NullReferenceException($"GuestImage with ID {id} not found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting GuestImage with ID {id}: {ex.Message}");
            }
            return false;
        }
    }
}
