using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Repo.DTOs;
using EventCheckinSystem.Repo.DTOs.CreateDTO;
using EventCheckinSystem.Repo.DTOs.ResponseDTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCheckinSystem.Services.Interfaces
{
    public interface IAuthenticateService
    {   
        Task<UserDTO> RegisterAsync(UserDTO userDTO);
        Task<LoginResponse> LoginAsync(string username, string password);
        Task<LoginResponse> LoginByGoogleAsync(string email);
        Task<LoginResponse> RefreshTokenAsync(string token, string refreshToken);
        Task<LoginResponse> RevokeRefreshTokenAsync(string refreshToken);
        Task<User> GetUserByEmailAsync(string email);
        Task<IEnumerable<UserResponse>> GetUsers();
        Task<User> GetUserByIdAsync(string userId);
        Task<string> GetUserEmailByIdAsync(string userId);
        Task<IEnumerable<User>> GetUsersByFullNameAsync(string fullName);
        Task<IdentityResult> UpdateUserAsync(UpdateUserDTO user);
        Task<IdentityResult> ChangeUserPasswordAsync(ChangePasswordDTO request);
        Task<bool> DeactiveUserAsync(string userId);
        Task<bool> ActiveUserAsync(string userId);
        Task<User?> GetUserByRefreshTokenAsync(string refreshToken);
        Task<Dictionary<string, string>> GetFullNamesByIdsAsync(IEnumerable<string> userIds);
    }

}
