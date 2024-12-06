using System.ComponentModel.DataAnnotations.Schema;

using Pms.Core.Database.Abstraction;

namespace Pms.Datalayer.Entities
{
    public class UserPerformanceReviewCompetencies : DbEntityIdBase
    {
        public Guid UserPerformanceReviewId { get; set; }
        public virtual UserPerformanceReview? UserPerformanceReview { get; set; }
        public Guid PerformanceReviewCompetencyId { get; set; }
        public virtual PerformanceReviewCompetency? PerformanceReviewCompetency { get; set; }
        public int? Value { get; set; } = 0;
        public string? Comment { get; set; } = string.Empty;
        public bool? IsManager { get; set; } = false;

    }
}
