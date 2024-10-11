using EventCheckinSystem.Repo.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCheckinSystem.Services.Interfaces
{
    public interface IUserEventServices
    {
        Task<List<UserEventDTO>> GetAllUserEventsAsync();
        Task<UserEventDTO> GetUserEventByIdAsync(int eventId);
        Task AddUserEventAsync(UserEventDTO userEventDto);
        Task UpdateUserEventAsync(UserEventDTO userEventDto);
        Task DeleteUserEventAsync(int eventId);
    }
}
