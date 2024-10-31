using EventCheckinSystem.Repo.DTOs;
using EventCheckinSystem.Repo.DTOs.CreateDTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCheckinSystem.Services.Interfaces
{
    public interface IGuestImageServices
    {
        Task<IEnumerable<GuestImageDTO>> GetAllGuestImagesAsync();
        Task<GuestImageDTO> GetGuestImageByIdAsync(int id);
        Task<GuestImageDTO> CreateGuestImageAsync(CreateGuestImageDTO guestImageDto);
        Task<bool> UpdateGuestImageAsync(GuestImageDTO guestImageDto);
        Task<bool> DeleteGuestImageAsync(int id);
    }
}
