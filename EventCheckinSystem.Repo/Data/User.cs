using EventCheckinSystem.Repo.Bases.BaseEntitys;
using Microsoft.AspNetCore.Identity;

namespace EventCheckinSystem.Repo.Data
{
    public class User : IdentityUser, IBaseEntity
    {
        public string? FullName { get; set; } = string.Empty;
        public string? ResetToken { get; set; }
        public string? ImagePath { get; set; }
        public string? EmailCode { get; set; }
        public DateTimeOffset? ResetTokenExpires { get; set; }
        public string? VerificationToken { get; set; }
        public DateTimeOffset? VerificationTokenExpires { get; set; }

        // Navigation properties
            public ICollection<Event> Events { get; set; }
        public ICollection<UserEvent> UserEvents { get; set; }

        // Base Entity
        public DateTimeOffset CreatedTime { get; set; }
        public DateTimeOffset LastUpdatedTime { get; set; }
        public DateTimeOffset? DeletedTime { get; set; }
        public string? CreatedBy { get; set; } = string.Empty;
        public string? LastUpdatedBy { get; set; } = string.Empty;
        public string? DeletedBy { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
    }
}
