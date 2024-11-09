using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Repo.DTOs;
using EventCheckinSystem.Repo.DTOs.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCheckinSystem.Repo.Repositories.Interfaces
{
    public interface IGuestImageRepo
    {
        Task<IEnumerable<GuestImage>> GetAllGuestImagesAsync();
        Task<GuestImage> GetGuestImageByIdAsync(int id);
        Task<GuestImage> CreateGuestImageAsync(GuestImage guestImage);
        Task<bool> UpdateGuestImageAsync(GuestImage guestImage);
        Task<bool> DeleteGuestImageAsync(int id);
        Task<PagedResult<GuestImage>> GetPagedGuestImagesAsync(PageRequest pageRequest);
        Task<GuestImage> GetGuestImageByGuestIdAsync(int guestId);
    }
}
