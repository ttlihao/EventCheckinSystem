using EventCheckinSystem.Repo.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCheckinSystem.Repo.Repositories.Interfaces
{
    public interface IGuestGroupRepo
    {
        Task<IEnumerable<GuestGroupDTO>> GetAllGuestGroupsAsync();
        Task<GuestGroupDTO> GetGuestGroupByIdAsync(int id);
        Task<GuestGroupDTO> CreateGuestGroupAsync(GuestGroupDTO guestGroupDto, string createdBy);
        Task UpdateGuestGroupAsync(GuestGroupDTO guestGroupDto, string updatedBy);
        Task DeleteGuestGroupAsync(int id);
        Task<GuestGroupDTO> GetGuestGroupByGuestIdAsync(int guestId);
    }
}
