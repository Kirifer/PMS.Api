namespace Pms.Core.Authentication
{
    internal class UserAccess : IRolePermission
    {
        public ICollection<string> GetPermissions()
        {
            return
            [
                // Resource Access
                AuthPermissions.AuthUserView,

                AuthPermissions.PmsUserView,
                AuthPermissions.PmsUserManage,
            ];
        }
    }
}
