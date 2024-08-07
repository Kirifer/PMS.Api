using System.Linq.Expressions;

using Pms.Core.Database.Abstraction;
using Pms.Datalayer.Interface;
using Pms.Shared.Enums;
using Pms.Shared.Exceptions;

using Microsoft.EntityFrameworkCore;
using Pms.Core.Abstraction;

namespace Pms.Datalayer.Implementation
{
    public class BaseRepository<TEntity> : DatalayerEntityService,
        IBaseRepository<TEntity> where TEntity : DbEntityIdBase
    {
        protected readonly PmsDbContext Context;
        protected readonly DbSet<TEntity> DbSet;

        protected BaseRepository(PmsDbContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var addedEntity = (await DbSet.AddAsync(entity)).Entity;
            await Context.SaveChangesAsync();

            return addedEntity;
        }

        public async Task<TEntity> DeleteAsync(TEntity entity)
        {
            var removedEntity = DbSet.Remove(entity).Entity;
            await Context.SaveChangesAsync();

            return removedEntity;
        }

        public async Task<List<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.Where(predicate).ToListAsync();
        }

        public async Task<TEntity> GetAsync(Guid id)
        {
            var entity = await DbSet.FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null) { throw new DatabaseAccessException(DbErrorCode.NotFound, $"Record is not found using the id: {id}"); }

            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            DbSet.Update(entity);
            await Context.SaveChangesAsync();

            return entity;
        }
    }
}
