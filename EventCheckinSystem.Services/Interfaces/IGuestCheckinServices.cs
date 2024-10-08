using EventCheckinSystem.Repo.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCheckinSystem.Services.Interfaces
{
    public interface IGuestCheckinServices
    {
        Task<IEnumerable<GuestCheckin>> GetAllCheckinsAsync();
        Task<GuestCheckin> GetCheckinByIdAsync(int id);
        Task<GuestCheckin> CreateCheckinAsync(GuestCheckin guestCheckin);
        Task UpdateCheckinAsync(GuestCheckin guestCheckin);
        Task DeleteCheckinAsync(int id);
        Task<GuestCheckin> CheckinGuestByIdAsync(int guestId, string created);
    }
}
