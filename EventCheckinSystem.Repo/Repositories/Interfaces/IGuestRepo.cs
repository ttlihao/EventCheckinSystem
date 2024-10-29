using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Repo.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCheckinSystem.Repo.Repositories.Interfaces
{
    public interface IGuestRepo
    {
        Task<List<Guest>> GetAllGuestsAsync();
        Task<Guest> GetGuestByIdAsync(int guestId);
        Task<Guest> AddGuestAsync(Guest guest);
        Task<bool> UpdateGuestAsync(Guest guest);
        Task<bool> DeleteGuestAsync(int guestId);
        Task<List<Guest>> GetGuestsByGroupIdAsync(int guestGroupId);
        Task<List<Guest>> GetGuestsByNameAsync(string guestName);
    }
}
