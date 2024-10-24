﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using Pms.Datalayer.Entities;

namespace Pms.DataLayer.Configurations
{
    public class PerformanceReviewConfiguration : IEntityTypeConfiguration<PerformanceReview>
    {
        public void Configure(EntityTypeBuilder<PerformanceReview> builder)
        {
            builder
                .HasMany(pr => pr.Goals)
                .WithOne(g => g.PerformanceReview)
                .HasPrincipalKey(pr => pr.Id)
                .HasForeignKey(g => g.PerformanceReviewId)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(pr => pr.Competencies)
                .WithOne(c => c.PerformanceReview)
                .HasPrincipalKey(pr => pr.Id)
                .HasForeignKey(c => c.PerformanceReviewId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
