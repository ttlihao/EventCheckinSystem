using System.Collections.Generic;
using System.Threading.Tasks;
using EventCheckinSystem.Repo.Data;

namespace EventCheckinSystem.Services.Interfaces
{
    public interface IUserEventServices
    {
        Task<IEnumerable<UserEvent>> GetAllUserEventsAsync();
        Task<UserEvent> GetUserEventAsync(string userId, int eventId);
        Task<UserEvent> CreateUserEventAsync(UserEvent userEvent);
        Task DeleteUserEventAsync(string userId, int eventId);
    }
}