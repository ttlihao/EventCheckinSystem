using EventCheckinSystem.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace EventCheckinSystem.Services.Services
{
    public class UserContextService : IUserContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserContextService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public string GetCurrentUserId()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            if (httpContext == null)
            {
                throw new ArgumentException("Http context is null. Please Login.");
            }

            var user = httpContext.User;
            if (user == null || !user.Identity.IsAuthenticated)
            {
                throw new ArgumentException("User is not Authenticated");
            }

            var currentUserId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(currentUserId))
            {
                throw new ArgumentException("User ID claim is not found");
            }
            return currentUserId;
        }
    }
}
