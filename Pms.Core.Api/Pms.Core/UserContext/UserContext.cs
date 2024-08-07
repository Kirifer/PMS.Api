using System.Security.Claims;

using Microsoft.AspNetCore.Http;

namespace Pms.Core.Authentication
{
    public class UserContext : IUserContext
    {
        public Guid UserId { get; set; }
        public Guid AuthId { get; set; }

        public string Email { get; set; }
        public IEnumerable<string> Roles { get; set; }
        public IEnumerable<string> Permissions { get; set; }

        public UserContext(IHttpContextAccessor httpContextAccessor)
        {
            SetUserIdentity(httpContextAccessor);
        }

        public bool HasPermission(params string[] requiredPermissions)
            => Permissions?.Intersect(requiredPermissions).Any() ?? false;

        private void SetUserIdentity(IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor?.HttpContext == null) return;

            var claimsIdentity = (ClaimsIdentity)httpContextAccessor.HttpContext.User.Identity!;

            Email = GetClaimValue(claimsIdentity, AuthClaims.Email);

            var userIdString = GetClaimValue(claimsIdentity, AuthClaims.UserId);
            if (!string.IsNullOrWhiteSpace(userIdString))
            {
                UserId = new Guid(userIdString);
            }

            var authIdString = GetClaimValue(claimsIdentity, AuthClaims.AuthId);
            if (!string.IsNullOrWhiteSpace(authIdString))
            {
                AuthId = new Guid(authIdString);
            }

            claimsIdentity.TryGetClaimsValue(AuthClaims.Role, out var roleClaims);
            Roles = roleClaims ?? new List<string>();
            Permissions = Roles
                .Where(role => !string.IsNullOrWhiteSpace(role))
                .SelectMany(role => RolePermissionFactory
                    .InitializeFactories()
                    .GetFactory(role)
                    .GetPermissions())
                .Distinct();
        }

        private string GetClaimValue(ClaimsIdentity claimsIdentity, string claimType)
        {
            var successfullyObtained = claimsIdentity.TryGetClaimValue(claimType, out var claimValue);
            return successfullyObtained ? claimValue : string.Empty;
        }
    }
}
