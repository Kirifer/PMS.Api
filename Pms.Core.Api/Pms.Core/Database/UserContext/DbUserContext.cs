using Pms.Core.Authentication;
using Pms.Shared.Extensions;

namespace Pms.Core.Database
{
    public class DbUserContext : IDbUserContext
    {
        public Guid UserId { get; set; }
        public string CompanyName { get; set; }
        public Guid AuthId { get; set; }
        public string UserEmail { get; set; }
        public bool IsAdmin { get; set; }

        public DbUserContext()
        {
            // For deserialization
        }

        public DbUserContext(IUserContext userContext)
        {
            AssignUserContext(userContext);
        }

        public void AssignUserContext(IUserContext userContext)
        {
            if (userContext == null) { return; }

            UserId = userContext.UserId;
            AuthId = userContext.AuthId;
            UserEmail = userContext.Email;

            if (!userContext.Roles.IsNullOrEmpty())
                IsAdmin = userContext.Roles.Contains(AuthRoles.Admin);
        }
    }
}
