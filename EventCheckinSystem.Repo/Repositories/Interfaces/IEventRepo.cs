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
        Task<IEnumerable<Event>> GetAllEventsAsync();
        Task<Event> GetEventByIdAsync(int id);
        Task<Event> CreateEventAsync(Event newEvent);
        Task<bool> UpdateEventAsync(Event updatedEvent);
        Task<bool> DeleteEventAsync(int id);
    }
}
