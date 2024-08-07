namespace Pms.Core.Authentication
{
    public static class AuthRoles
    {
        // For overall platform roles
        public const string Admin = "Admin";
        public const string User = "User";
    }

    public static class AuthPermissions
    {
        #region Auth platform permissions
        public const string AuthUserView = "AuthUserView";                                          // Can only view users from authentication
        public const string AuthUserManage = "AuthUserManage";                                      // Can manage users
        #endregion

        #region Pms platform permissions
        public const string PmsUserManage = "PmsUserManage";                                     // Can manage users on ats
        public const string PmsUserView = "PmsUserView";                                         // Can view its own details/resources
        #endregion
    }

    public static class AuthClaims
    {
        public const string UserId = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier";
        public const string AuthId = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/authenticationidentifier";
        public const string Email = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress";
        public const string Role = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role";
    }
}
