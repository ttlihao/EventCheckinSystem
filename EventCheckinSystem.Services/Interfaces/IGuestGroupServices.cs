﻿using EventCheckinSystem.Repo.DTOs;
using EventCheckinSystem.Repo.DTOs.ResponseDTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EventCheckinSystem.Services.Interfaces
{
    public interface IGuestGroupServices
    {
        Task<IEnumerable<GuestGroupResponse>> GetAllGuestGroupsAsync();
        Task<GuestGroupResponse> GetGuestGroupByIdAsync(int id);
        Task<GuestGroupResponse> CreateGuestGroupAsync(GuestGroupDTO guestGroupDto);
        Task<bool> UpdateGuestGroupAsync(GuestGroupDTO guestGroupDto);
        Task<bool> DeleteGuestGroupAsync(int id);
        Task<GuestGroupResponse> GetGuestGroupByGuestIdAsync(int guestId);
    }
}
