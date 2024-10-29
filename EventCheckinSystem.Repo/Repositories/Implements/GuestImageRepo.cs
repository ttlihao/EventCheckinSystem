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

        public async Task UpdateGuestImageAsync(GuestImage guestImage)
        {
            try
            {
                var existingImage = await _context.GuestImages.FindAsync(guestImage.GuestImageID);
                if (existingImage != null)
                {
                    existingImage.GuestID = guestImage.GuestID;
                    existingImage.ImageURL = guestImage.ImageURL;
                    existingImage.LastUpdatedTime = guestImage.LastUpdatedTime;

                    _context.GuestImages.Update(existingImage);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new NullReferenceException($"GuestImage with ID {guestImage.GuestImageID} not found.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating GuestImage: {ex.Message}");
            }
        }

        public async Task DeleteGuestImageAsync(int id)
        {
            try
            {
                var imageToDelete = await _context.GuestImages.FindAsync(id);
                if (imageToDelete != null)
                {
                    _context.GuestImages.Remove(imageToDelete);
                    await _context.SaveChangesAsync();
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
        }
    }
}
