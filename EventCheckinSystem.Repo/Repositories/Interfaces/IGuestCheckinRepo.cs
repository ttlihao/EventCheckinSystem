using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Repo.DTOs;
using EventCheckinSystem.Repo.DTOs.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCheckinSystem.Repo.Repositories.Interfaces
{
    public interface IGuestCheckinRepo
    {
        Task<IEnumerable<GuestCheckin>> GetAllCheckinsAsync();
        Task<GuestCheckin> GetCheckinByIdAsync(int id);
        Task<GuestCheckin> CreateCheckinAsync(GuestCheckin guestCheckin);
        Task<bool> UpdateCheckinAsync(GuestCheckin updatedCheckinDto);
        Task<bool> DeleteCheckinAsync(int id);
        Task<GuestCheckin> CheckinGuestByIdAsync(int guestId, string createdBy);
        Task<PagedResult<GuestCheckin>> GetPagedCheckinsAsync(PageRequest pageRequest);
    }
}

