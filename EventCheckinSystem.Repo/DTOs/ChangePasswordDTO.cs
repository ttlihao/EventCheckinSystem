using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCheckinSystem.Repo.DTOs
{
    public class ChangePasswordDTO
    {
        public required string Username { get; set; }
        public required string OldPassword { get; set; }
        public required string NewPassword { get; set; }
        public required string ConfirmedPassword { get; set; }
    }
}
