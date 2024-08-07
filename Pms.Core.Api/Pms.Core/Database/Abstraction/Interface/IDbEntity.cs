namespace Pms.Core.Database.Abstraction.Interface
{
    public interface IDbEntity
    {
        Guid Id { get; set; }

        DateTimeOffset CreatedOn { get; set; }

        Guid? CreatorId { get; set; }

        DateTimeOffset? UpdatedOn { get; set; }

        Guid? UpdaterId { get; set; }
    }
}
