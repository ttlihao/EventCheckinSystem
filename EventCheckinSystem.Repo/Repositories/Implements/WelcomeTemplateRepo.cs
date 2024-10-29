using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Repo.DTOs;
using EventCheckinSystem.Repo.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventCheckinSystem.Repo.Repositories.Implements
{
    public class WelcomeTemplateRepo : IWelcomeTemplateRepo
    {
        private readonly EventCheckinManagementContext _context;

        public WelcomeTemplateRepo(EventCheckinManagementContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WelcomeTemplate>> GetAllWelcomeTemplatesAsync()
        {
            return await _context.WelcomeTemplates
                .Where(x => x.IsActive && !x.IsDelete)
                                 .ToListAsync();
        }

        public async Task<WelcomeTemplate> GetWelcomeTemplateByIdAsync(int id)
        {
            try
            {
                var template = await _context.WelcomeTemplates.FirstOrDefaultAsync(x => x.IsActive && !x.IsDelete && x.WelcomeTemplateID == id);
                if (template == null)
                {
                    throw new NullReferenceException($"WelcomeTemplate with ID {id} not found.");
                }
                return template;
            }
            catch (Exception ex)
            {
                // Log the exception
                // Logger.LogError(ex.Message);
                throw new Exception($"Error retrieving WelcomeTemplate with ID {id}: {ex.Message}");
            }
        }


        public async Task<WelcomeTemplate> CreateWelcomeTemplateAsync(WelcomeTemplate welcomeTemplate)
        {
            await _context.WelcomeTemplates.AddAsync(welcomeTemplate);
            await _context.SaveChangesAsync();
            return welcomeTemplate;
        }

        public async Task<bool> UpdateWelcomeTemplateAsync(WelcomeTemplate welcomeTemplate)
        {
            bool isSuccess = false;
            try
            {
                var existingWelcomeTemplate = await _context.WelcomeTemplates.FirstOrDefaultAsync(e => e.WelcomeTemplateID == welcomeTemplate.WelcomeTemplateID);
                if (existingWelcomeTemplate != null)
                {
                    _context.Entry(existingWelcomeTemplate).State = EntityState.Detached; // Detach the existing entity
                    _context.WelcomeTemplates.Attach(welcomeTemplate);
                    _context.Entry(welcomeTemplate).State = EntityState.Modified; // Mark as modified
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

        public async Task<bool> DeleteWelcomeTemplateAsync(int id)
        {
            var templateToDelete = await _context.WelcomeTemplates.FindAsync(id);
            if (templateToDelete != null)
            {
                _context.WelcomeTemplates.Remove(templateToDelete);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<IEnumerable<WelcomeTemplate>> GetWelcomeTemplatesByGuestGroupAsync(int guestGroupId)
        {
            return await _context.WelcomeTemplates
                                 .Where(w => w.GuestGroupID == guestGroupId && !w.IsDelete && w.IsActive)
                                 .ToListAsync();
        }
    }
}
