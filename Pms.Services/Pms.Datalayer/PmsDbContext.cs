using Microsoft.EntityFrameworkCore;

using Pms.Core.Database;
using Pms.Core.Database.Abstraction;
using Pms.Core.Database.Abstraction.Interface;
using Pms.Datalayer.Entities;

namespace Pms.Datalayer
{
    public class PmsDbContext : DbContextBase
    {
        public DbSet<User> Users { get; set; }

        public DbSet<PmsCompetency> Competencies { get; set; }
        public DbSet<PerformanceReview> PerformanceReviews { get; set; }
        public DbSet<PerformanceReviewCompetency> PerformanceReviewCompetencies { get; set; }
        public DbSet<PerformanceReviewGoal> PerformanceReviewGoals { get; set; }

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
