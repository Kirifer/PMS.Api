using Pms.Core.Database.Abstraction;

namespace Pms.Datalayer.Entities
{
    public class PerformanceReviewCompetency : DbEntityIdBase
    {
        public Guid PerformanceReviewId { get; set; }
        public virtual PerformanceReview? PerformanceReview { get; set; }
        public Guid CompetencyId { get; set; }
        public virtual PmsCompetency? Competency { get; set; }
        public decimal? Weight { get; set; } = 0M;
        public int OrderNo { get; set; } = 0;
    }
}
