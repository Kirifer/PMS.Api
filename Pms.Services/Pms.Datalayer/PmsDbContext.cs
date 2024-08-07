using Pms.Core.Database;
using Pms.Core.Database.Abstraction;
using Pms.Core.Database.Abstraction.Interface;
using Pms.Datalayer.Entities;

using Microsoft.EntityFrameworkCore;

namespace Pms.Datalayer
{
    public class PmsDbContext : DbContextBase
    {
        public DbSet<User> Users { get; set; }

        public PmsDbContext(DbContextOptions<DbContextBase> options)
           : base(options)
        {
        }

        public PmsDbContext(IDbConfig dbConfig, IDbUserContext dbUserContext, IDbMigration dbMigration)
            : base(dbConfig, dbUserContext, dbMigration)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
