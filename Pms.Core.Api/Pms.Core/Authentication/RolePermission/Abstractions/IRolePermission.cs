namespace Pms.Core.Authentication
{
    public interface IRolePermission
    {
        ICollection<string> GetPermissions();
    }
}
