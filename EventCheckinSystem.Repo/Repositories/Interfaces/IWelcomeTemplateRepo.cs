using EventCheckinSystem.Repo.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCheckinSystem.Repo.Repositories.Interfaces
{
    public interface IWelcomeTemplateRepo
    {
        Task<IEnumerable<WelcomeTemplate>> GetAllWelcomeTemplatesAsync();
        Task<WelcomeTemplate> GetWelcomeTemplateByIdAsync(int id);
        Task<WelcomeTemplate> CreateWelcomeTemplateAsync(WelcomeTemplate welcomeTemplateDto);
        Task<bool> UpdateWelcomeTemplateAsync(WelcomeTemplate welcomeTemplateDto);
        Task<bool> DeleteWelcomeTemplateAsync(int id);
        Task<IEnumerable<WelcomeTemplate>> GetWelcomeTemplatesByGuestGroupAsync(int guestGroupId);
        Task<(IEnumerable<WelcomeTemplate> templates, int totalCount)> GetWelcomeTemplatesPagedAsync(int pageNumber, int pageSize);
    }
}
