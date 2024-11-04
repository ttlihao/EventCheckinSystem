using AutoMapper;
using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Repo.DTOs;
using EventCheckinSystem.Repo.DTOs.Paging;
using EventCheckinSystem.Repo.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCheckinSystem.Repo.Repositories.Implements
{
    public class GuestCheckinRepo : IGuestCheckinRepo
    {
        private readonly EventCheckinManagementContext _context;

        public GuestCheckinRepo(EventCheckinManagementContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<GuestCheckin>> GetAllCheckinsAsync()
        {
            try
            {
                var checkins = await _context.GuestCheckins
                    .Include(gc => gc.Guest)
                    .Where(e => e.IsActive && !e.IsDelete)
                    .ToListAsync();
                return (checkins);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving all guest check-ins: {ex.Message}");
            }
        }

        public async Task<GuestCheckin> GetCheckinByIdAsync(int id)
        {
            try
            {
                var guestCheckin = await _context.GuestCheckins
                    .Where(e => e.IsActive && !e.IsDelete)
                    .Include(gc => gc.Guest)
                    .FirstOrDefaultAsync(gc => gc.GuestCheckinID == id);

                if (guestCheckin == null)
                {
                    throw new NullReferenceException($"Guest check-in with ID {id} not found.");
                }

                return (guestCheckin);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving guest check-in with ID {id}: {ex.Message}");
            }
        }

        public async Task<GuestCheckin> CreateCheckinAsync(GuestCheckin guestCheckin)
        {
            try
            {
                await _context.GuestCheckins.AddAsync(guestCheckin);
                await _context.SaveChangesAsync();
                return  guestCheckin;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error creating guest check-in: {ex.Message}");
            }
        }

        public async Task<bool> UpdateCheckinAsync(GuestCheckin updatedCheckinDto)
        {
            bool isSuccess = false;
            try
            {
                var existingCheckin = await _context.GuestCheckins.FirstOrDefaultAsync(e => e.GuestCheckinID == updatedCheckinDto.GuestCheckinID);
                if (existingCheckin != null)
                {
                    _context.Entry(existingCheckin).State = EntityState.Detached; // Detach the existing entity
                    var updatedCheckin = (updatedCheckinDto);
                    _context.GuestCheckins.Attach(updatedCheckin);
                    _context.Entry(updatedCheckin).State = EntityState.Modified; // Mark as modified
                    var changes = await _context.SaveChangesAsync();
                    isSuccess = changes > 0; // Return true if changes were made
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Error updating guest check-in: {e.Message}");
            }
            return isSuccess;
        }

        public async Task<bool> DeleteCheckinAsync(int id)
        {
            try
            {
                var checkinToDelete = await _context.GuestCheckins.FindAsync(id);
                if (checkinToDelete != null)
                {
                    _context.GuestCheckins.Remove(checkinToDelete);
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting guest check-in with ID {id}: {ex.Message}");
            }
        }

        public async Task<GuestCheckin> CheckinGuestByIdAsync(int guestId, string createdBy)
        {
            try
            {
                var checkin = new GuestCheckin
                {
                    GuestID = guestId,
                    CheckinTime = DateTime.UtcNow,
                    Status = "Completed",
                    CreatedBy = createdBy,
                    LastUpdatedBy = createdBy
                };
                await _context.GuestCheckins.AddAsync(checkin);
                await _context.SaveChangesAsync();
                return (checkin);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error checking in guest with ID {guestId}: {ex.Message}");
            }
        }

        public async Task<PagedResult<GuestCheckin>> GetPagedCheckinsAsync(PageRequest pageRequest)
        {
            var query = _context.GuestCheckins
                .Where(e => e.IsActive && !e.IsDelete)
                .Include(gc => gc.Guest);

            return await query.CreatePagingAsync(pageRequest.PageNumber, pageRequest.PageSize);
        }
    }
}
