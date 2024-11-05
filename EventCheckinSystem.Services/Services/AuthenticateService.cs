using AutoMapper;
using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Repo.DTOs;
using EventCheckinSystem.Repo.DTOs.ResponseDTO;
using EventCheckinSystem.Repo.Repositories.Interfaces;
using EventCheckinSystem.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

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
        private readonly IMapper _mapper;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthenticateService(RoleManager<IdentityRole> roleManager, IMapper mapper, IAuthenticateRepo authenticateRepo, IConfiguration configuration, UserManager<User> userManager, ITimeService timeService)
        {
            _authenticateRepo = authenticateRepo;
            _configuration = configuration;
            _userManager = userManager;
            _exAccessToken = int.Parse(_configuration["Jwt:ExpirationAccessToken"]!);
            _exRefreshToken = int.Parse(_configuration["Jwt:ExpirationRefreshToken"]!);
            _timeService = timeService;
            _mapper = mapper;
            _roleManager = roleManager;
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

        public async Task<UserDTO> RegisterAsync(UserDTO userDTO)
        {
            if (await _userManager.Users.AnyAsync(x => x.UserName == userDTO.Username))
                throw new ArgumentException("User đã tồn tại.");



            //user
            User newUser = _mapper.Map<User>(userDTO);
            newUser.IsActive = true;
            newUser.IsDelete = false;
            IdentityResult resultCreateUser = await _userManager.CreateAsync(newUser, userDTO.Password);
            if (!resultCreateUser.Succeeded)
                throw new ArgumentException("Đã có lỗi xảy ra");

            // Role by ID
            var roleID = userDTO.RoleID;
            IdentityRole? getRoleID = await _roleManager.FindByIdAsync(roleID);

            if (getRoleID == null)
            {
                throw new ArgumentException("Vai trò không tồn tại.");
            }

            IdentityResult resultAddRoleID = await _userManager.AddToRoleAsync(newUser, getRoleID.Name!);

            if (!resultAddRoleID.Succeeded)
            {
                throw new ArgumentException("Không thể thêm vai trò cho người dùng.");
            }


            return _mapper.Map<UserDTO>(newUser);
        }

        public async Task<LoginResponse> LoginAsync(string username, string password)
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
                var userRole = roles.FirstOrDefault();
                if (userRole == null)
                {
                    throw new ArgumentException("Người dùng chưa được cấp quyền");
                }

                // Generate access token
                var accessToken = GenerateToken(user.Id, userRole, false);
                // Generate refresh token
                var refreshToken = GenerateToken(user.Id, userRole, true);

                // Save Database
                user.VerificationToken = accessToken;
                user.ResetToken = refreshToken;
                user.VerificationTokenExpires = _timeService.SystemTimeNow.AddHours(_exRefreshToken);
                user.VerificationTokenExpires = _timeService.SystemTimeNow.AddHours(_exRefreshToken);
                user.ResetTokenExpires = _timeService.SystemTimeNow.AddHours(_exAccessToken);
                await _userManager.UpdateAsync(user);
                return new LoginResponse
                {
                    VerificationToken = accessToken,
                    ResetToken = refreshToken,
                    UserId = user.Id,
                    Username = user.UserName,
                    Email = user.Email,
                    RoleName = userRole
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public Task<LoginResponse> RefreshTokenAsync(string token, string refreshToken)
        {
            throw new NotImplementedException();
        }

        public Task<LoginResponse> RevokeRefreshTokenAsync(string refreshToken)
        {
            throw new NotImplementedException();
        }

        public async Task<User> GetUserByIdAsync(string userId)
        {
            try
            {
                var user = await _authenticateRepo.GetUsesByIdAsync(userId);
                if (user == null)
                {
                    throw new ArgumentException("Không tìm thấy người dùng");
                }

                return user;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        private string GenerateToken(string userId, string role, bool isRefreshToken)
        {
            try
            {
                var keyString = _configuration["Jwt:Key"]
                     ?? throw new ArgumentException("JWT key is not configured.");
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

        public async Task<User> GetUserByEmailAsync(string email)
        {
            try
            {
                var user = await _authenticateRepo.GetUserByEmailAsync(email);
                if (user == null)
                {
                    throw new ArgumentException("Không tìm thấy người dùng");
                }

                return user;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<LoginResponse> LoginByGoogleAsync(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email) ??
                    throw new ArgumentException("Email not registered yet!");

                var roles = await _userManager.GetRolesAsync(user);
                var userRole = roles.FirstOrDefault();
                if (userRole == null)
                {
                    throw new ArgumentException("Người dùng chưa được cấp quyền");
                }

                // Generate access token
                var accessToken = GenerateToken(user.Id, userRole, false);
                // Generate refresh token
                var refreshToken = GenerateToken(user.Id, userRole, true);

                // Save Database
                user.VerificationToken = accessToken;
                user.ResetToken = refreshToken;
                user.VerificationTokenExpires = _timeService.SystemTimeNow.AddHours(_exRefreshToken);
                user.VerificationTokenExpires = _timeService.SystemTimeNow.AddHours(_exRefreshToken);
                user.ResetTokenExpires = _timeService.SystemTimeNow.AddHours(_exAccessToken);
                await _userManager.UpdateAsync(user);
                return new LoginResponse
                {
                    VerificationToken = accessToken,
                    ResetToken = refreshToken,
                    UserId = user.Id,
                    Username = user.UserName,
                    Email = user.Email,
                    RoleName = userRole
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
