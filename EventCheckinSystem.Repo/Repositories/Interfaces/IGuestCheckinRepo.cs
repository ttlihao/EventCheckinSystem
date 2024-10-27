using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Repo.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCheckinSystem.Repo.Repositories.Interfaces
{
    public interface IGuestCheckinRepo
    {
        Task<IEnumerable<GuestCheckinDTO>> GetAllCheckinsAsync();
        Task<GuestCheckinDTO> GetCheckinByIdAsync(int id);
        Task<GuestCheckinDTO> CreateCheckinAsync(GuestCheckinDTO guestCheckinDto, string createdBy);
        Task UpdateCheckinAsync(GuestCheckinDTO updatedCheckinDto);
        Task DeleteCheckinAsync(int id);
        Task<GuestCheckinDTO> CheckinGuestByIdAsync(int guestId, string createdBy);
    }
}

