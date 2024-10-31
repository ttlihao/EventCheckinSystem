using AutoMapper;
using Azure.Core;
using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Repo.DTOs;
using EventCheckinSystem.Repo.DTOs.CreateDTO;
using EventCheckinSystem.Repo.DTOs.ResponseDTO;
using EventCheckinSystem.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCheckinSystem.Services.Services
{
    public class RoleService : IRoleService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;

        public RoleService(RoleManager<IdentityRole> roleManager, IMapper mapper, UserManager<User> userManager)
        {
            _roleManager = roleManager;
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<bool> AddUserToRoleAsync(UserRoleDTO userRole)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userRole.UserId);
                if (user == null)
                {
                    throw new ArgumentException("Không tìm thấy người dùng");
                }
                //by id
                var roleExists = await _roleManager.FindByIdAsync(userRole.RoleId);
                if (roleExists == null)
                {
                    throw new ArgumentException("Không tìm thấy vai trò");
                }
                var result = await _userManager.AddToRoleAsync(user, roleExists.Name);
                return result.Succeeded;
            }
            catch
            {
                throw new ArgumentException("Đã xảy ra lỗi");
            }
        }

        public async Task<bool> CreateRoleAsync(string roleName)
        {
            try
            {
                var result = await _roleManager.CreateAsync(new IdentityRole(roleName.ToLower()));
                if (!result.Succeeded)
                {
                    throw new ArgumentException("Vai trò đã tồn tại.");
                }

                return result.Succeeded;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> DeleteRoleAsync(string roleId)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(roleId.ToLower());
                if (role == null)
                {
                    throw new ArgumentException("Vai trò không tồn tại.");
                }

                var result = await _roleManager.DeleteAsync(role);
                return result.Succeeded;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<RoleResponse>> GetAllRolesAsync()
        {
            try
            {
                // Get all roles using RoleManager
                var roles = await _roleManager.Roles.ToListAsync();

                // Map roles to GetRoleResponse objects
                return _mapper.Map<IEnumerable<RoleResponse>>(roles);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<UserRoleDTO>> GetAllUserRoleAsync()
        {
            try
            {
                var allUserRoles = new List<UserRoleDTO>();
                var users = _userManager.Users.ToList();
                foreach (var user in users)
                {
                    var roles = await _userManager.GetRolesAsync(user);
                    foreach (var role in roles)
                    {
                        var roleEntity = await _roleManager.FindByNameAsync(role);
                        if (roleEntity != null)
                        {
                            allUserRoles.Add(new UserRoleDTO { UserId = user.Id, RoleId = roleEntity.Id });
                        }
                    }
                }
                return allUserRoles;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<RoleResponse> GetRoleByNameAsync(string roleName)
        {
            try
            {
                var role = await _roleManager.FindByNameAsync(roleName.ToLower());
                if (role == null)
                {
                    throw new ArgumentException("Vai trò không tồn tại.");
                }
                return _mapper.Map<RoleResponse>(role);

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<string>> GetUserRolesAsync(string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    throw new ArgumentException("Không tìm thấy người dùng");
                }

                var roles = await _userManager.GetRolesAsync(user);
                return roles.ToList();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
            public async Task<bool> RemoveUserFromRoleAsync(UserRoleDTO userRole)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userRole.UserId);
                if (user == null)
                {
                    throw new ArgumentException("Không tìm thấy người dùng");
                }

                var role = await _roleManager.FindByIdAsync(userRole.RoleId);
                if (role == null)
                {
                    throw new ArgumentException("Không tìm thấy vai trò");
                }

                var result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                return result.Succeeded;
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public async Task<bool> UpdateRoleAsync(UpdateRoleDTO updateRequest)
        {
            try
            {
                var role = await _roleManager.FindByIdAsync(updateRequest.RoleId);
                if (role == null)
                {
                    throw new ArgumentException("Không tìm thấy vai trò");
                }
                role.Name = updateRequest.NewRoleName.ToLower();
                var result = await _roleManager.UpdateAsync(role);
                return result.Succeeded;
            }

            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
