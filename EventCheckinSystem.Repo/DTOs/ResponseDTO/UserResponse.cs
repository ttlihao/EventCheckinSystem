using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventCheckinSystem.Repo.DTOs
{
    public class UserResponse
    {
        public string Id { get; set; }
        public required string Email { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string FullName { get; set; }
        public required string PhoneNumber { get; set; }
        public required string RoleName { get; set; }
    }
}
