using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Repo.DTOs;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCheckinSystem.Services.Interfaces
{
    public interface IGuestServices
    {
        Task<List<GuestDTO>> GetAllGuestsAsync();
        Task<GuestDTO> GetGuestByIdAsync(int guestId);
        Task<GuestDTO> AddGuestAsync(GuestDTO guest);
        Task<bool> UpdateGuestAsync(GuestDTO guest);
        Task<bool> DeleteGuestAsync(int guestId);
        Task<List<GuestDTO>> GetGuestsByGroupIdAsync(int guestGroupId);
        Task<List<GuestDTO>> GetGuestByNameAsync(string guestName);
        Task<List<GuestDTO>> GetGuestsByEventIdAsync(int eventId);
        Task<int> ImportGuestsFromExcelAsync(IFormFile file);
    }
}
