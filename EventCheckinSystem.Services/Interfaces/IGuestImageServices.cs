using EventCheckinSystem.Repo.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCheckinSystem.Services.Interfaces
{
    public interface IGuestImageServices
    {
        Task<IEnumerable<GuestImageDTO>> GetAllGuestImagesAsync();
        Task<GuestImageDTO> GetGuestImageByIdAsync(int id);
        Task<GuestImageDTO> CreateGuestImageAsync(GuestImageDTO guestImageDto);
        Task UpdateGuestImageAsync(GuestImageDTO guestImageDto);
        Task DeleteGuestImageAsync(int id);
    }
}
