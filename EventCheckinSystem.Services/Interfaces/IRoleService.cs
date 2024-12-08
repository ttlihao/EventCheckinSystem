﻿using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Repo.DTOs;
using EventCheckinSystem.Repo.DTOs.CreateDTO;
using EventCheckinSystem.Repo.DTOs.ResponseDTO;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCheckinSystem.Services.Interfaces
{
    public interface IRoleService
    {
            Task<IEnumerable<RoleResponse>> GetAllRolesAsync();
            Task<RoleResponse> GetRoleByNameAsync(string roleName);
            Task<bool> CreateRoleAsync(string roleName);
            Task<bool> UpdateRoleAsync(UpdateRoleDTO role);
            Task<bool> DeleteRoleAsync(string roleId);
            Task<bool> AddUserToRoleAsync(UserRoleDTO userRole);
            Task<bool> RemoveUserFromRoleAsync(UserRoleDTO userRole);
            Task<List<string>> GetUserRolesAsync(string userId);
            Task<List<UserRoleDTO>> GetAllUserRoleAsync(); 

    }
}
