namespace Pms.Core.Database.Abstraction.Interface
{
    public interface IDbContext
    {
        IDbUserContext UserContext { get; }
    }
}
