namespace Pms.Core.Authentication
{
    internal class AdminAccess : IRolePermission
    {
        public ICollection<string> GetPermissions()
        {
            return
            [
                // Resource Access
                AuthPermissions.AuthUserView,
                AuthPermissions.AuthUserManage,

                AuthPermissions.PmsUserView,
                AuthPermissions.PmsUserManage,
            ];
        }
    }
}
