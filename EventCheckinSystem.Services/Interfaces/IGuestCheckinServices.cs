﻿using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Repo.DTOs;
using EventCheckinSystem.Repo.DTOs.CreateDTO;
using EventCheckinSystem.Repo.DTOs.Paging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCheckinSystem.Services.Interfaces
{
    public interface IGuestCheckinServices
    {
        Task<IEnumerable<GuestCheckinDTO>> GetAllCheckinsAsync();
        Task<GuestCheckinDTO> GetCheckinByIdAsync(int id);
        Task<GuestCheckinDTO> CreateCheckinAsync(int guestId);
        Task<bool> UpdateCheckinAsync(GuestCheckinDTO updatedCheckinDto);
        Task<bool> DeleteCheckinAsync(int id);
        Task<GuestCheckinDTO> CheckinGuestByIdAsync(int guestId);
        Task<PagedResult<GuestCheckinDTO>> GetPagedCheckinsAsync(PageRequest pageRequest);
    }
}

