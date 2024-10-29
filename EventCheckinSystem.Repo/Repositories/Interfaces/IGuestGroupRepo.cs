using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Repo.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCheckinSystem.Repo.Repositories.Interfaces
{
    public interface IGuestGroupRepo
    {
        Task<IEnumerable<GuestGroup>> GetAllGuestGroupsAsync();
        Task<GuestGroup> GetGuestGroupByIdAsync(int id);
        Task<GuestGroup> CreateGuestGroupAsync(GuestGroup guestGroup);
        Task<bool> UpdateGuestGroupAsync(GuestGroup guestGroup);
        Task<bool> DeleteGuestGroupAsync(int id);
        Task<GuestGroup> GetGuestGroupByGuestIdAsync(int guestId);
    }
}
