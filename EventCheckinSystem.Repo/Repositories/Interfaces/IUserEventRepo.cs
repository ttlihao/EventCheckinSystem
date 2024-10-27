using EventCheckinSystem.Repo.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCheckinSystem.Repo.Repositories.Interfaces
{
    public interface IUserEventRepo
    {
        Task<List<UserEventDTO>> GetAllUserEventsAsync();
        Task<UserEventDTO> GetUserEventByIdAsync(int eventId);
        Task AddUserEventAsync(UserEventDTO userEventDto);
        Task UpdateUserEventAsync(UserEventDTO userEventDto);
        Task DeleteUserEventAsync(int eventId);
    }
}
