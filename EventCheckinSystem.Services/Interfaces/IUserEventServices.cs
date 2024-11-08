using EventCheckinSystem.Repo.DTOs;
using EventCheckinSystem.Repo.DTOs.Paging;
using EventCheckinSystem.Repo.DTOs.ResponseDTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCheckinSystem.Services.Interfaces
{
    public interface IUserEventServices
    {
        Task<List<UserEventResponse>> GetAllUserEventsAsync();
        Task<UserEventResponse> GetUserEventByIdAsync(int eventId);
        Task<UserEventResponse> AddUserEventAsync(UserEventDTO userEventDto);
        Task<bool> UpdateUserEventAsync(UserEventDTO userEventDto);
        Task<bool> DeleteUserEventAsync(int eventId);
        Task<PagedResult<UserEventResponse>> GetPagedUserEventsAsync(PageRequest pageRequest);
        Task<List<EventResponse>> GetEventsByUserIdAsync(string userId);
    }
}
