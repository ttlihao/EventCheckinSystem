using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Repo.DTOs.ResponseDTO;
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
        Task<LoginResponseDTO> RegisterAsync(string email, string password);
        Task<LoginResponseDTO> LoginAsync(string email, string password);
        Task<LoginResponseDTO> RefreshTokenAsync(string token, string refreshToken);
        Task<LoginResponseDTO> RevokeRefreshTokenAsync(string refreshToken);
        Task<User> GetUserByIdAsync(string userId);
        Task<string> GetUserEmailByIdAsync(string userId);
        Task<IEnumerable<User>> GetUsersByFullNameAsync(string fullName);
        Task UpdateUserAsync(User user);
        Task<User?> GetUserByRefreshTokenAsync(string refreshToken);
        Task<Dictionary<string, string>> GetFullNamesByIdsAsync(IEnumerable<string> userIds);
    }

}
