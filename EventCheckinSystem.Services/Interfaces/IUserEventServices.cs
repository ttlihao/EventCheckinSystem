using EventCheckinSystem.Repo.DTOs;
using EventCheckinSystem.Repo.DTOs.Paging;
using EventCheckinSystem.Repo.DTOs.ResponseDTO;
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
        Task<List<EventResponse>> GetEventsByUserIdAsync(string userId);
    }
}
