using EventCheckinSystem.Repo.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCheckinSystem.Services.Interfaces
{
    public interface IGuestGroupServices
    {
        Task<IEnumerable<GuestGroupDTO>> GetAllGuestGroupsAsync();
        Task<GuestGroupDTO> GetGuestGroupByIdAsync(int id);
        Task<GuestGroupDTO> CreateGuestGroupAsync(GuestGroupDTO guestGroupDto, string createdBy);
        Task UpdateGuestGroupAsync(GuestGroupDTO guestGroupDto, string updatedBy);
        Task DeleteGuestGroupAsync(int id);
        Task<GuestGroupDTO> GetGuestGroupByGuestIdAsync(int guestId);
    }
}
