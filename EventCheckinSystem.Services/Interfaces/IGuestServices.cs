using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Repo.DTOs;
using EventCheckinSystem.Repo.DTOs.CreateDTO;
using EventCheckinSystem.Repo.DTOs.Paging;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCheckinSystem.Services.Interfaces
{
    public interface IGuestServices
    {
        Task<List<GuestDTO>> GetAllGuestsAsync();
        Task<GuestDTO> GetGuestByIdAsync(int guestId);
        Task<GuestDTO> AddGuestAsync(CreateGuestDTO guestDto, IFormFile imageFile);
        Task<bool> UpdateGuestAsync(GuestDTO guest);
        Task<bool> DeleteGuestAsync(int guestId);
        Task<List<GuestDTO>> GetGuestsByGroupIdAsync(int guestGroupId);
        Task<List<GuestDTO>> GetGuestByNameAsync(string guestName);
        Task<List<GuestDTO>> GetGuestsByEventIdAsync(int eventId);
        Task<PagedResult<GuestDTO>> GetPagedGuestsAsync(PageRequest pageRequest);

    }
}
