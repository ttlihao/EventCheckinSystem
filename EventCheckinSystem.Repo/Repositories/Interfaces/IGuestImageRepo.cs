using EventCheckinSystem.Repo.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCheckinSystem.Repo.Repositories.Interfaces
{
    public interface IGuestImageRepo
    {
        Task<IEnumerable<GuestImageDTO>> GetAllGuestImagesAsync();
        Task<GuestImageDTO> GetGuestImageByIdAsync(int id);
        Task<GuestImageDTO> CreateGuestImageAsync(GuestImageDTO guestImageDto);
        Task UpdateGuestImageAsync(GuestImageDTO guestImageDto);
        Task DeleteGuestImageAsync(int id);
    }
}
