﻿using EventCheckinSystem.Repo.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCheckinSystem.Repo.Repositories.Interfaces
{
    public interface IAuthenticateRepo
    {
        Task<IEnumerable<User>> GetUsersByFullNameAsync(string fullName);
        Task<string> GetUserEmailByIdAsync(string userId);
        Task UpdateAsync(User user);
        Task<User?> GetUserByRefreshTokenAsync(string refreshToken);
        Task UpdateUserAsync(User user);
        Task<string?> GetUserNameByIdAsync(object id);
        Task<Dictionary<string, string>> GetFullNamesByIdsAsync(IEnumerable<string> userIds);
    }

}
