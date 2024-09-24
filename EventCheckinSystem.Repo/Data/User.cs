using EventCheckinSystem.Repo.Bases.BaseEntitys;
using Microsoft.AspNetCore.Identity;

namespace EventCheckinSystem.Repo.Data
{
    public class User : IdentityUser, IBaseEntity
    {
        public required string FullName { get; set; } = string.Empty;
        public string? ResetToken { get; set; }
        public string? ImagePath { get; set; }
        public string? EmailCode { get; set; }
        public DateTimeOffset? ResetTokenExpires { get; set; }
        public string? VerificationToken { get; set; }
        public DateTimeOffset? VerificationTokenExpires { get; set; }

        // Navigation properties


        // Base Entity
        public DateTimeOffset CreatedTime { get; set; }
        public DateTimeOffset LastUpdatedTime { get; set; }
        public DateTimeOffset? DeletedTime { get; set; }
        public required string CreatedBy { get; set; } = string.Empty;
        public required string LastUpdatedBy { get; set; } = string.Empty;
        public string? DeletedBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}
