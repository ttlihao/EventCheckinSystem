using EventCheckinSystem.Repo.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCheckinSystem.Services.Interfaces
{
    public interface IGuestGroupServices
    {
        Task<IEnumerable<GuestGroupDTO>> GetAllGuestGroupsAsync();
        Task<GuestGroupDTO> GetGuestGroupByIdAsync(int id);
        Task<GuestGroupDTO> CreateGuestGroupAsync(GuestGroupDTO guestGroupDto);
        Task<bool> UpdateGuestGroupAsync(GuestGroupDTO guestGroupDto);
        Task<bool> DeleteGuestGroupAsync(int id);
        Task<GuestGroupDTO> GetGuestGroupByGuestIdAsync(int guestId);
    }
}
