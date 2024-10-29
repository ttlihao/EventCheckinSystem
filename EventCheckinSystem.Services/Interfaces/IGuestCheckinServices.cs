using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Repo.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCheckinSystem.Services.Interfaces
{
    public interface IGuestCheckinServices
    {
        Task<IEnumerable<GuestCheckinDTO>> GetAllCheckinsAsync();
        Task<GuestCheckinDTO> GetCheckinByIdAsync(int id);
        Task<GuestCheckinDTO> CreateCheckinAsync(GuestCheckinDTO guestCheckinDto);
        Task<bool> UpdateCheckinAsync(GuestCheckinDTO updatedCheckinDto);
        Task<bool> DeleteCheckinAsync(int id);
        Task<GuestCheckinDTO> CheckinGuestByIdAsync(int guestId);
    }
}

