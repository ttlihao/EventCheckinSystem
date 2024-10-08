using EventCheckinSystem.Repo.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCheckinSystem.Services.Interfaces
{
    public interface IGuestImageServices
    {
        Task<IEnumerable<GuestImage>> GetAllGuestImagesAsync();
        Task<GuestImage> GetGuestImageByIdAsync(int id);
        Task<GuestImage> CreateGuestImageAsync(GuestImage guestImage);
        Task UpdateGuestImageAsync(GuestImage guestImage);
        Task DeleteGuestImageAsync(int id);
    }
}
