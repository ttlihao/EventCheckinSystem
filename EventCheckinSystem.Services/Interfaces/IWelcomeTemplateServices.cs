using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Repo.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCheckinSystem.Services.Interfaces
{
    public interface IWelcomeTemplateServices
    {
        Task<IEnumerable<WelcomeTemplateDTO>> GetAllWelcomeTemplatesAsync();
        Task<WelcomeTemplateDTO> GetWelcomeTemplateByIdAsync(int id);
        Task<WelcomeTemplate> CreateWelcomeTemplateAsync(WelcomeTemplateDTO welcomeTemplateDto, string createdBy);
        Task UpdateWelcomeTemplateAsync(WelcomeTemplateDTO welcomeTemplateDto, string updatedBy);
        Task DeleteWelcomeTemplateAsync(int id);
        Task<IEnumerable<WelcomeTemplateDTO>> GetWelcomeTemplatesByGuestGroupAsync(int guestGroupId);
    }
}
