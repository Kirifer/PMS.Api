namespace Pms.Core.Authentication
{
    public class RolePermissionFactory
    {
        private readonly IDictionary<string, IRolePermission> _rolePermissions;

        private RolePermissionFactory()
        {
            _rolePermissions = new Dictionary<string, IRolePermission>()
            {
                { AuthRoles.Admin, new AdminAccess() },
                { AuthRoles.User, new UserAccess() },
            };
        }

        public static RolePermissionFactory InitializeFactories() => new();

        public IRolePermission GetFactory(string role)
        {
            var isObtained = _rolePermissions.TryGetValue(role, out var entityFactory);
            if (!isObtained) throw new KeyNotFoundException();
            return entityFactory;
        }
    }
}
