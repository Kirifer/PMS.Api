using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Pms.Datalayer.Entities;

namespace Pms.DataLayer.Configurations
{
    public class PerformanceReviewCompetencyConfiguration : IEntityTypeConfiguration<PerformanceReviewCompetency>
    {
        public void Configure(EntityTypeBuilder<PerformanceReviewCompetency> builder)
        {
            builder
                .HasOne(pr => pr.Competency)
                .WithOne()
                .HasPrincipalKey<PerformanceReviewCompetency>(pr => pr.CompetencyLevelId)
                .HasForeignKey<PmsCompetency>(c => c.Id)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
