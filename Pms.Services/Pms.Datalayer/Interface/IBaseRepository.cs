using System.Linq.Expressions;

using Pms.Core.Abstraction;
using Pms.Core.Database.Abstraction;

namespace Pms.Datalayer.Interface
{
    public interface IBaseRepository<TEntity> : IDatalayerEntityService
        where TEntity : DbEntityIdBase
    {
        Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity> GetAsync(Guid id);

        Task<TEntity> AddAsync(TEntity entity);

        Task<TEntity> UpdateAsync(TEntity entity);

        Task<TEntity> DeleteAsync(TEntity entity);
    }
}
