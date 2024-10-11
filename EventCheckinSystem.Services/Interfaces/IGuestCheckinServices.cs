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
        Task<GuestCheckinDTO> CreateCheckinAsync(GuestCheckinDTO guestCheckinDto, string createdBy);
        Task UpdateCheckinAsync(GuestCheckinDTO updatedCheckinDto);
        Task DeleteCheckinAsync(int id);
        Task<GuestCheckinDTO> CheckinGuestByIdAsync(int guestId, string createdBy);
    }
}

