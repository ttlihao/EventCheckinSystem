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
        Task UpdateGuestGroupAsync(GuestGroup guestGroupDto, string updatedBy);
        Task DeleteGuestGroupAsync(int id);
        Task<GuestGroup> GetGuestGroupByGuestIdAsync(int guestId);
    }
}
