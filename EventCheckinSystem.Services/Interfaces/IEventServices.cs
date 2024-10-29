using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Repo.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCheckinSystem.Services.Interfaces
{
    public interface IEventServices
    {
        Task<IEnumerable<EventDTO>> GetAllEventsAsync();
        Task<EventDTO> GetEventByIdAsync(int id);
        Task<EventDTO> CreateEventAsync(CreateEventDTO newEvent);
        Task<bool> UpdateEventAsync(EventDTO updatedEvent);
        Task<bool> DeleteEventAsync(int id);
    }
}
