using EventCheckinSystem.Repo.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCheckinSystem.Repo.Repositories.Interfaces
{
    public interface IUserEventRepo
    {
        Task<List<UserEvent>> GetAllUserEventsAsync();
        Task<UserEvent> GetUserEventByIdAsync(int eventId);
        Task<UserEvent> AddUserEventAsync(UserEvent userEvent);
        Task<bool> UpdateUserEventAsync(UserEvent userEvent);
        Task<bool> DeleteUserEventAsync(int eventId);
    }
}
