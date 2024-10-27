using Azure.Core;
using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Repo.DTOs;
using EventCheckinSystem.Repo.Repositories.Interfaces;
using EventCheckinSystem.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EventCheckinSystem.Services.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly IAuthenticateRepo _authenticateRepo;
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly int _exAccessToken;
        private readonly int _exRefreshToken;
        private readonly ITimeService _timeService;

        public AuthenticateService(IAuthenticateRepo authenticateRepo, IConfiguration configuration, UserManager<User> userManager, ITimeService timeService)
        {
            _authenticateRepo = authenticateRepo;
            _configuration = configuration;
            _userManager = userManager;
            _exAccessToken = int.Parse(_configuration["Jwt:ExpirationAccessToken"]!);
            _exRefreshToken = int.Parse(_configuration["Jwt:ExpirationRefreshToken"]!);
            _timeService = timeService;
        }

        public async Task<IEnumerable<User>> GetUsersByFullNameAsync(string fullName)
        {
            return await _authenticateRepo.GetUsersByFullNameAsync(fullName);
        }

        public async Task<string> GetUserEmailByIdAsync(string userId)
        {
            return await _authenticateRepo.GetUserEmailByIdAsync(userId);
        }

        public async Task UpdateAsync(User user)
        {
            await _authenticateRepo.UpdateAsync(user);
        }

        public async Task<User?> GetUserByRefreshTokenAsync(string refreshToken)
        {
            return await _authenticateRepo.GetUserByRefreshTokenAsync(refreshToken);
        }

        public async Task UpdateUserAsync(User user)
        {
            await _authenticateRepo.UpdateUserAsync(user);
        }

        public async Task<string?> GetUserNameByIdAsync(object id)
        {
            return await _authenticateRepo.GetUserNameByIdAsync(id);
        }

        public async Task<Dictionary<string, string>> GetFullNamesByIdsAsync(IEnumerable<string> userIds)
        {
            return await _authenticateRepo.GetFullNamesByIdsAsync(userIds);
        }

        public Task<LoginResponseDTO> RegisterAsync(string email, string password)
        {
            throw new NotImplementedException();
        }

        public async Task<LoginResponseDTO> LoginAsync(string username, string password)
        {
            try
            {
                var user = await _userManager.FindByNameAsync(username) ??
                    throw new ArgumentException("Wrong username");
                var checkPassword = await _userManager.CheckPasswordAsync(user, password);
                if (!checkPassword)
                {
                    throw new ArgumentException("Wrong password"); 
                }

                var roles = await _userManager.GetRolesAsync(user);
                /* var userRole = roles.FirstOrDefault();
                 if (userRole == null)
                 {
                     throw new ErrorException(StatusCodes.Status404NotFound, ResponseCodeConstants.NOT_FOUND, "Tên đăng nhập hoặc mật khẩu không đúng");
                 }*/

                // Generate access token
                var accessToken = GenerateToken(user.Id, "Admin", false);
                // Generate refresh token
                var refreshToken = GenerateToken(user.Id, "Admin", true);

                // Save Database
                user.VerificationToken = accessToken;
                user.ResetToken = refreshToken;
                user.VerificationTokenExpires = _timeService.SystemTimeNow.AddHours(_exRefreshToken);
                user.VerificationTokenExpires = _timeService.SystemTimeNow.AddHours(_exRefreshToken);
                user.ResetTokenExpires = _timeService.SystemTimeNow.AddMinutes(_exAccessToken);
                await _userManager.UpdateAsync(user);
                return new LoginResponseDTO
                {
                    VerificationToken = accessToken,
                    ResetToken = refreshToken,
                    UserId = user.Id,
                    Username = user.UserName,
                    Email = user.Email,
                    /*Role = userRole*/
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<LoginResponseDTO> RefreshTokenAsync(string token, string refreshToken)
        {
            throw new NotImplementedException();
        }

        public Task<LoginResponseDTO> RevokeRefreshTokenAsync(string refreshToken)
        {
            throw new NotImplementedException();
        }

        public Task<User> GetUserByIdAsync(string userId)
        {
            throw new NotImplementedException();
        }
        private string GenerateToken(string userId, string role, bool isRefreshToken)
        {
            try
            {
                var keyString = _configuration["Jwt:Key"];
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
                var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>
                {
                    new Claim("Id", userId),
                    new Claim(ClaimTypes.NameIdentifier, userId),
                    new Claim(ClaimTypes.Role, role),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                if (isRefreshToken)
                {
                    claims.Add(new Claim("isRefreshToken", "true"));
                }

                DateTime expiresDateTime;
                if (isRefreshToken)
                {
                    expiresDateTime = _timeService.SystemTimeNow.AddHours(_exRefreshToken).DateTime;
                }
                else
                {
                    expiresDateTime = _timeService.SystemTimeNow.AddMinutes(_exAccessToken).DateTime;
                }


                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: expiresDateTime,
                    signingCredentials: credentials
                );

                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
