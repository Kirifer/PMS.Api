using Pms.Core.Database.Abstraction.Interface;

namespace Pms.Core.Database.Abstraction
{
    public interface IDbQuerySingle { }

    public interface IDbQuerySingle<TEntity, TCriteria> : IDbQuerySingle
    {
        //IDbQuerySingle<TEntity, TCriteria> SetDbUserContext(IDbUserContext userContext);

        TEntity Get(TCriteria criteria);
    }

    public abstract class DbQuerySingleBase<TEntity, TCriteria>(IDbContext dbContext)
        : IDbQuerySingle<TEntity, TCriteria>
        where TEntity : class, new()
    {
        protected IDbContext DbContext { get; } = dbContext;
        //protected IDbUserContext UserContext { get; private set; } = dbContext.UserContext;

        protected TCriteria _criteria;

        protected abstract TEntity Build();

        public TEntity Get(TCriteria criteria)
        {
            _criteria = criteria;
            var dataRecords = Build();
            return dataRecords;
        }

        //public IDbQuerySingle<TEntity, TCriteria> SetDbUserContext(IDbUserContext userContext)
        //{
        //    UserContext = userContext;
        //    return this;
        //}
    }
}
