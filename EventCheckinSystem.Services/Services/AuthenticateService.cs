using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCheckinSystem.Services.Services
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly EventCheckinManagementContext _dbContext;

        public AuthenticateService(EventCheckinManagementContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<User>> GetUsersByFullNameAsync(string fullName)
        {
            var searchTerm = fullName.Trim().ToLower();
            var users = await _dbContext.Users
                .Where(u => u.FullName.ToLower().Contains(searchTerm) && u.IsDelete == false && u.IsActive == true)
                .ToListAsync();
            return users;
        }

        public async Task<string> GetUserEmailByIdAsync(string userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);
            return user?.Email;
        }

        public async Task UpdateAsync(User user)
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<User?> GetUserByRefreshTokenAsync(string resetToken)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.ResetToken == resetToken);
        }

        public async Task UpdateUserAsync(User user)
        {
            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<string?> GetUserNameByIdAsync(object id)
        {
            var user = await _dbContext.Set<User>().FindAsync(id);
            if (user == null)
            {
                return null;
            }
            return user.FullName;
        }

        public async Task<Dictionary<string, string>> GetFullNamesByIdsAsync(IEnumerable<string> userIds)
        {
            // Ensure that the list of userIds is not empty
            if (!userIds.Any())
            {
                return new Dictionary<string, string>();
            }

            // Query the database for user names
            var users = await _dbContext.Users
                .Where(user => userIds.Contains(user.Id))
                .Select(user => new { user.Id, user.FullName })
                .ToListAsync();

            // Convert the list of users to a dictionary
            return users.ToDictionary(u => u.Id, u => u.FullName);
        }
    }
}
