using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCheckinSystem.Repo.DTOs.ResponseDTO
{
    public class LoginResponse
    {
        public string VerificationToken { get; set; }
        public string ResetToken { get; set; }
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string RoleName { get; set; }
    }
}
