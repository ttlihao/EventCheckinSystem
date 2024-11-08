using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Repo.DTOs;
using EventCheckinSystem.Repo.DTOs.CreateDTO;
using EventCheckinSystem.Repo.DTOs.Paging;
using EventCheckinSystem.Repo.DTOs.ResponseDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCheckinSystem.Services.Interfaces
{
    public interface IEventServices
    {
        Task<IEnumerable<EventResponse>> GetAllEventsAsync();
        Task<EventResponse> GetEventByIdAsync(int id);
        Task<int> GetTotalEventByMonth(int month, int year);
        Task<EventResponse> CreateEventAsync(CreateEventDTO newEvent);
        Task<bool> UpdateEventAsync(EventDTO updatedEvent);
        Task<bool> DeleteEventAsync(int id);
        Task<PagedResult<EventResponse>> GetPagedEventsAsync(PageRequest pageRequest);
        Task<PagedResult<EventResponse>> GetPagedIncomingEventsAsync(PageRequest pageRequest);
        Task<PagedResult<EventResponse>> GetEventByMonth(int month, int year);
    }
}
