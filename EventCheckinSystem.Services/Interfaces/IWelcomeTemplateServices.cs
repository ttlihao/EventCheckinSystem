using System.Collections.Generic;
using System.Threading.Tasks;
using EventCheckinSystem.Repo.Data;

namespace EventCheckinSystem.Services.Interfaces
{
    public interface IWelcomeTemplateServices
    {
        Task<IEnumerable<WelcomeTemplate>> GetAllWelcomeTemplatesAsync();
        Task<WelcomeTemplate> GetWelcomeTemplateByIdAsync(int id);
        Task<WelcomeTemplate> CreateWelcomeTemplateAsync(WelcomeTemplate welcomeTemplate);
        Task UpdateWelcomeTemplateAsync(WelcomeTemplate updatedWelcomeTemplate);
        Task DeleteWelcomeTemplateAsync(int id);
        Task<IEnumerable<WelcomeTemplate>> GetWelcomeTemplatesByGuestGroupAsync(int guestGroupId);
    }
}
