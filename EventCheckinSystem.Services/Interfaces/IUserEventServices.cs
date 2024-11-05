using EventCheckinSystem.Repo.DTOs;
using EventCheckinSystem.Repo.DTOs.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCheckinSystem.Services.Interfaces
{
    public interface IUserEventServices
    {
        Task<List<UserEventDTO>> GetAllUserEventsAsync();
        Task<UserEventDTO> GetUserEventByIdAsync(int eventId);
        Task<UserEventDTO> AddUserEventAsync(UserEventDTO userEventDto);
        Task<bool> UpdateUserEventAsync(UserEventDTO userEventDto);
        Task<bool> DeleteUserEventAsync(int eventId);
        Task<PagedResult<UserEventDTO>> GetPagedUserEventsAsync(PageRequest pageRequest);
        Task<List<EventDTO>> GetEventsByUserIdAsync(string userId);
    }
}
