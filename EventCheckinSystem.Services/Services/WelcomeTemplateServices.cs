using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Repo.DTOs;
using EventCheckinSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<WelcomeTemplateDTO>> GetAllWelcomeTemplatesAsync()
        {
            return await _context.WelcomeTemplates
                                 .Select(w => new WelcomeTemplateDTO
                                 {
                                     WelcomeTemplateID = w.WelcomeTemplateID,
                                     GuestGroupID = w.GuestGroupID,
                                     TemplateContent = w.TemplateContent,
                                     GuestGroupName = w.GuestGroup.Name // Assuming you have a navigation property
                                 })
                                 .ToListAsync();
        }

        public async Task<WelcomeTemplateDTO> GetWelcomeTemplateByIdAsync(int id)
        {
            var template = await _context.WelcomeTemplates.FindAsync(id);
            if (template == null) return null;

            return new WelcomeTemplateDTO
            {
                WelcomeTemplateID = template.WelcomeTemplateID,
                GuestGroupID = template.GuestGroupID,
                TemplateContent = template.TemplateContent,
                GuestGroupName = template.GuestGroup.Name // Assuming you have a navigation property
            };
        }

        public async Task<WelcomeTemplate> CreateWelcomeTemplateAsync(WelcomeTemplateDTO welcomeTemplateDto, string createdBy)
        {
            var welcomeTemplate = new WelcomeTemplate
            {
                GuestGroupID = welcomeTemplateDto.GuestGroupID,
                TemplateContent = welcomeTemplateDto.TemplateContent,
                CreatedBy = createdBy, // Set the createdBy
                LastUpdatedBy = createdBy // Set lastUpdatedBy
            };

            await _context.WelcomeTemplates.AddAsync(welcomeTemplate);
            await _context.SaveChangesAsync();
            return welcomeTemplate;
        }

        public async Task UpdateWelcomeTemplateAsync(WelcomeTemplateDTO welcomeTemplateDto, string updatedBy)
        {
            var existingTemplate = await _context.WelcomeTemplates.FindAsync(welcomeTemplateDto.WelcomeTemplateID);

            if (existingTemplate != null)
            {
                existingTemplate.GuestGroupID = welcomeTemplateDto.GuestGroupID;
                existingTemplate.TemplateContent = welcomeTemplateDto.TemplateContent;
                existingTemplate.LastUpdatedBy = updatedBy; // Update lastUpdatedBy

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

        public async Task<IEnumerable<WelcomeTemplateDTO>> GetWelcomeTemplatesByGuestGroupAsync(int guestGroupId)
        {
            return await _context.WelcomeTemplates
                                 .Where(w => w.GuestGroupID == guestGroupId)
                                 .Select(w => new WelcomeTemplateDTO
                                 {
                                     WelcomeTemplateID = w.WelcomeTemplateID,
                                     GuestGroupID = w.GuestGroupID,
                                     TemplateContent = w.TemplateContent,
                                     GuestGroupName = w.GuestGroup.Name // Assuming you have a navigation property
                                 })
                                 .ToListAsync();
        }
    }
}
