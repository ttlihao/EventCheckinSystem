using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Repo.DTOs;
using EventCheckinSystem.Repo.DTOs.CreateDTO;
using EventCheckinSystem.Repo.DTOs.Paging;
using EventCheckinSystem.Repo.DTOs.ResponseDTO;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCheckinSystem.Services.Interfaces
{
    public interface IGuestServices
    {
        Task<List<GuestResponse>> GetAllGuestsAsync();
        Task<GuestResponse> GetGuestByIdAsync(int guestId);
        Task<GuestDTO> AddGuestAsync(CreateGuestDTO guestDto, IFormFile imageFile);
        Task<bool> UpdateGuestAsync(GuestDTO guestDto, IFormFile imageFile = null);
        Task<bool> DeleteGuestAsync(int guestId);
        Task<List<GuestResponse>> GetGuestsByGroupIdAsync(int guestGroupId);
        Task<List<GuestResponse>> GetGuestByNameAsync(string guestName);
        Task<List<GuestResponse>> GetGuestsByEventIdAsync(int eventId);
        Task<PagedResult<GuestResponse>> GetPagedGuestsAsync(PageRequest pageRequest);

    }
}
