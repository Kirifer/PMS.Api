using System.Diagnostics;

using Microsoft.EntityFrameworkCore;

using Pms.Core.Database.Abstraction.Interface;

namespace Pms.Core.Database.Abstraction
{
    public class DbContextBase : DbContext, IDbContext
    {
        private readonly IDbConfig _dbConfig;
        private readonly IDbUserContext _userContext;
        private readonly IDbMigration _dbMigration;

        public IDbUserContext UserContext => _userContext;

        public DbContextBase(DbContextOptions<DbContextBase> options)
            : base(options)
        {
        }

        public DbContextBase(
            IDbConfig dbConfig,
            IDbUserContext userContext,
            IDbMigration dbMigration)
            : base(new DbContextOptionsBuilder<DbContext>().UseNpgsql(
                dbConfig.ConnectionString,
                opt => opt.MigrationsHistoryTable("EfMigrationHistory".ToSnakeCase()))
                  .UseSnakeCaseNamingConvention().Options)
        {
            _dbConfig = dbConfig;
            _userContext = userContext;
            _dbMigration = dbMigration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseSnakeCase();
            modelBuilder.RegisterEntityConfigurations();

            base.OnModelCreating(modelBuilder);
        }

        /// <summary>
        /// Save changes async of all database entity
        /// </summary>
        /// <param name="cancellationToken">Cancellation token state</param>
        /// <returns>Returns status of savechanges result</returns>
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                if (!entry.Entity.GetType().IsAssignableTo(typeof(DbEntityIdBase))) { continue; }

                var entity = (DbEntityIdBase)entry.Entity;
                if (Equals(entity, null)) continue;

                UpdateEntityBaseInfo(entity, entry.State);
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        /// <summary>
        /// Detaches the entity from context. This should call after saving the entity to
        /// prevent the EFCore in attaching the entity to the context after processed and throws
        /// No Tracking issue. Please refer to this link https://github.com/dotnet/efcore/issues/12459
        /// </summary>
        public void DetachEntitiesFromContext()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                entry.State = EntityState.Detached;
            }
        }

        /// <summary>
        /// Updates any pending migration to database
        /// </summary>
        public void UpdateDatabaseMigration()
        {
            try
            {
                if (Database.GetPendingMigrations().Any())
                {
                    Database.Migrate();
                    Debug.WriteLine($"{GetType().Name} Successfully migrated the database.");
                }
                else
                {
                    Debug.WriteLine($"{GetType().Name} Database is up to date.");
                }

                _dbMigration.ExecuteMigration();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void UpdateEntityBaseInfo<TEntity>(TEntity entity, EntityState entityState)
        {
            if (Equals(entity, null)) return;

            var dbConfigType = entity.GetType().GetInterface(nameof(IDbEntity));
            if (Equals(dbConfigType, null)) return;

            var dbEntity = entity as IDbEntity;
            if (entityState == EntityState.Added)
            {
                dbEntity.CreatorId = UserContext.UserId;
                if (dbEntity.CreatedOn == new DateTimeOffset())
                {
                    dbEntity.CreatedOn = DateTimeOffset.UtcNow;
                }
            }
            else if (entityState == EntityState.Modified)
            {
                dbEntity.UpdaterId = UserContext.UserId;
                dbEntity.UpdatedOn = DateTimeOffset.UtcNow;
            }
        }
    }
}
