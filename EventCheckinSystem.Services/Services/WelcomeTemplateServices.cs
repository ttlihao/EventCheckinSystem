using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCheckinSystem.Services.Services
{
    public class WelcomeTemplateServices : IWelcomeTemplateServices
    {
        private readonly EventCheckinManagementContext _context;

        public WelcomeTemplateServices(EventCheckinManagementContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<WelcomeTemplate>> GetAllWelcomeTemplatesAsync()
        {
            return await _context.WelcomeTemplates
                                 .Include(wt => wt.GuestGroup)
                                 .ToListAsync();
        }

        public async Task<WelcomeTemplate> GetWelcomeTemplateByIdAsync(int id)
        {
            return await _context.WelcomeTemplates
                                 .Include(wt => wt.GuestGroup)
                                 .FirstOrDefaultAsync(wt => wt.WelcomeTemplateID == id);
        }

        public async Task<WelcomeTemplate> CreateWelcomeTemplateAsync(WelcomeTemplate welcomeTemplate)
        {
            await _context.WelcomeTemplates.AddAsync(welcomeTemplate);
            await _context.SaveChangesAsync();
            return welcomeTemplate;
        }

        public async Task UpdateWelcomeTemplateAsync(WelcomeTemplate updatedWelcomeTemplate)
        {
            var existingTemplate = await _context.WelcomeTemplates.FindAsync(updatedWelcomeTemplate.WelcomeTemplateID);

            if (existingTemplate != null)
            {
                existingTemplate.GuestGroupID = updatedWelcomeTemplate.GuestGroupID;
                existingTemplate.TemplateContent = updatedWelcomeTemplate.TemplateContent;

                _context.WelcomeTemplates.Update(existingTemplate);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteWelcomeTemplateAsync(int id)
        {
            var templateToDelete = await _context.WelcomeTemplates.FindAsync(id);
            if (templateToDelete != null)
            {
                _context.WelcomeTemplates.Remove(templateToDelete);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<WelcomeTemplate>> GetWelcomeTemplatesByGuestGroupAsync(int guestGroupId)
        {
            return await _context.WelcomeTemplates
                                 .Include(wt => wt.GuestGroup)
                                 .Where(wt => wt.GuestGroupID == guestGroupId)
                                 .ToListAsync();
        }

    }
}
