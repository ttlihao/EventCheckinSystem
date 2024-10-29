using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Repo.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCheckinSystem.Repo.Repositories.Interfaces
{
    public interface IGuestImageRepo
    {
        Task<IEnumerable<GuestImage>> GetAllGuestImagesAsync();
        Task<GuestImage> GetGuestImageByIdAsync(int id);
        Task<GuestImage> CreateGuestImageAsync(GuestImage guestImage);
        Task UpdateGuestImageAsync(GuestImage guestImage);
        Task DeleteGuestImageAsync(int id);
    }
}
