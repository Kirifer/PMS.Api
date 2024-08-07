using System.Linq.Expressions;

using Pms.Core.Database.Abstraction.Interface;

namespace Pms.Core.Database.Abstraction
{
    public interface IDbQuery { }

    public interface IDbQuery<TEntity, TCriteria> : IDbQuery
    {
        IQueryable<TEntity> GetQuery(TCriteria criteria);

        IQueryable<TEntity> GetQuery(
            TCriteria criteria,
            Expression<Func<TEntity, TEntity>> selector);

        IQueryable<TSingleProperty> GetQuery<TSingleProperty>(
            TCriteria criteria,
            Expression<Func<TEntity, TSingleProperty>> selector);

        //IDbQuery<TEntity, TCriteria> SetDbUserContext(IDbUserContext userContext);

        int GetTotalCount();
    }

    public abstract class DbQueryBase<TEntity, TCriteria>(IDbContext dbContext)
        : IDbQuery<TEntity, TCriteria>
        where TEntity : class, new()
    {
        protected IDbContext DbContext { get; } = dbContext;
        //protected IDbUserContext UserContext { get; private set; } = dbContext.UserContext;

        protected TCriteria _criteria;

        private int _total;


        protected abstract IQueryable<TEntity> BuildQuery();

        protected IDbQuery<TEntity, TCriteria> SetTotalCount(int count)
        {
            _total = count;
            return this;
        }

        //public IDbQuery<TEntity, TCriteria> SetDbUserContext(IDbUserContext userContext)
        //{
        //    UserContext = userContext;
        //    return this;
        //}

        public virtual IQueryable<TEntity> GetQuery(TCriteria criteria)
        {
            _criteria = criteria;
            var query = BuildQuery();
            _total = query.Count();

            return query;
        }

        public virtual IQueryable<TEntity> GetQuery(
            TCriteria criteria,
            Expression<Func<TEntity, TEntity>> selector)
        {
            var query = GetQuery(criteria);
            return query.Select(selector);
        }

        public virtual IQueryable<TSingleProperty> GetQuery<TSingleProperty>(
            TCriteria criteria,
            Expression<Func<TEntity, TSingleProperty>> selector)
        {
            var query = GetQuery(criteria);
            return query.Select(selector);
        }

        public int GetTotalCount()
        {
            return _total;
        }
    }
}
