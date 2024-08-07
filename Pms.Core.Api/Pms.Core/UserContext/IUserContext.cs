namespace Pms.Core.Authentication
{
    public interface IUserContext
    {
        Guid UserId { get; set; }

        Guid AuthId { get; set; }

        string Email { get; set; }

        IEnumerable<string> Roles { get; set; }

        IEnumerable<string> Permissions { get; set; }

        bool HasPermission(params string[] requiredPermissions);
    }
}
