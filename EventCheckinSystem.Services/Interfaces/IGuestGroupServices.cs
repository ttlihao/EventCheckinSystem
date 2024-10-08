using EventCheckinSystem.Repo.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCheckinSystem.Services.Interfaces
{
    public interface IGuestGroupServices
    {
        Task<IEnumerable<GuestGroup>> GetAllGuestGroupsAsync();
        Task<GuestGroup> GetGuestGroupByIdAsync(int id);
        Task<GuestGroup> CreateGuestGroupAsync(GuestGroup guestGroup);
        Task UpdateGuestGroupAsync(GuestGroup guestGroup);
        Task DeleteGuestGroupAsync(int id);
        Task<GuestGroup> GetGuestGroupByGuestIdAsync(int guestId);
    }
}
