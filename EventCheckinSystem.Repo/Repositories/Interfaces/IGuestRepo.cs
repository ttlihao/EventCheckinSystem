using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Repo.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCheckinSystem.Repo.Repositories.Interfaces
{
    public interface IGuestRepo
    {
        Task<List<GuestDTO>> GetAllGuestsAsync();
        Task<GuestDTO> GetGuestByIdAsync(int guestId);
        Task AddGuestAsync(Guest guest, string createdBy);
        Task UpdateGuestAsync(Guest guest, string updatedBy);
        Task DeleteGuestAsync(int guestId);
        Task<List<GuestDTO>> GetGuestsByGroupIdAsync(int guestGroupId);
    }
}
