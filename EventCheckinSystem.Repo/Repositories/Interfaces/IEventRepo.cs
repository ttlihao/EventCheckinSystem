using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Repo.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCheckinSystem.Repo.Repositories.Interfaces
{
    public interface IEventRepo
    {
        Task<IEnumerable<EventDTO>> GetAllEventsAsync();
        Task<EventDTO> GetEventByIdAsync(int id);
        Task<EventDTO> CreateEventAsync(EventDTO newEvent);
        Task UpdateEventAsync(EventDTO updatedEvent);
        Task DeleteEventAsync(int id);
    }
}
