using EventCheckinSystem.Repo.Data;
using EventCheckinSystem.Repo.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCheckinSystem.Repo.Repositories.Implements
{
    public class AuthenticateRepo : IAuthenticateRepo
    {
        private readonly EventCheckinManagementContext _dbContext;

        public AuthenticateRepo(EventCheckinManagementContext dbContext)
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
        public async Task<IEnumerable<User>> GetAllUser()
        {
            var users = await _dbContext.Users
                .Where(u =>  u.IsDelete == false && u.IsActive == true)
                .ToListAsync();
            return users;
        }

        public async Task<string> GetUserEmailByIdAsync(string userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);
            return user?.Email;
        }


        public async Task<User?> GetUserByRefreshTokenAsync(string resetToken)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(u => u.ResetToken == resetToken);
        }

        public async Task<bool> UpdateUserAsync(User user)
        {
            _dbContext.Users.Update(user); 
            var result = await _dbContext.SaveChangesAsync(); 
            return result > 0;
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

        public async Task<User> GetUsesByIdAsync(string userId)
        {
            var searchTerm = userId.Trim().ToLower();
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Id.ToLower().Contains(searchTerm) && u.IsDelete == false && u.IsActive == true);
            return user;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            var searchTerm = email.Trim().ToLower();
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Email.ToLower().Equals(searchTerm) && u.IsDelete == false && u.IsActive == true);
            return user;
        }
    }
}
