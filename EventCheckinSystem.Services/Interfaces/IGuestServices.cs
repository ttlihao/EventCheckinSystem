using EventCheckinSystem.Repo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCheckinSystem.Services.Interfaces
{
    public interface IGuestServices
    {
        Task<List<Guest>> GetAllGuestsAsync();
        Task<Guest> GetGuestByIdAsync(int guestId);
        Task AddGuestAsync(Guest guest);
        Task UpdateGuestAsync(Guest guest);
        Task DeleteGuestAsync(int guestId);
        Task<List<Guest>> GetGuestsByGroupIdAsync(int guestGroupId);

    }
}
