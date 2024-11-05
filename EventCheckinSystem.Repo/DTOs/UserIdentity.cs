using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EventCheckinSystem.Repo.DTOs
{
    public class UserIdentity { 
        public string Name { get; set; } 
        public string Email { get; set; } 
        public UserIdentity(ClaimsIdentity identity)
        { 
            if (identity != null) 
            { 
                Name = identity.FindFirst(ClaimTypes.Name)?.Value; 
                Email = identity.FindFirst(ClaimTypes.Email)?.Value; } 
        } 
    }
}
