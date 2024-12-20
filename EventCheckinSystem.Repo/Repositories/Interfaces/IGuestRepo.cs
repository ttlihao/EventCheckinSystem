﻿using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Repo.DTOs;
using EventCheckinSystem.Repo.DTOs.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCheckinSystem.Repo.Repositories.Interfaces
{
    public interface IGuestRepo
    {
        Task<List<Guest>> GetAllGuestsAsync();
        Task<Guest> GetGuestByIdAsync(int guestId);
        Task<Guest> AddGuestAsync(Guest guest);
        Task<bool> UpdateGuestAsync(Guest guest);
        Task<bool> DeleteGuestAsync(int guestId);
        Task<List<Guest>> GetGuestsByGroupIdAsync(int guestGroupId);
        Task<List<Guest>> GetGuestsByNameAsync(string guestName);
        Task<List<Guest>> GetGuestsByEventIdAsync(int eventId);
        Task AddGuestsAsync(List<Guest> guests);
        Task<bool> GuestGroupExistsAsync(int guestGroupId);
        Task<PagedResult<Guest>> GetPagedGuestsAsync(PageRequest pageRequest);
    }
}
