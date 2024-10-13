using Pms.Core.Database.Abstraction;

namespace Pms.Datalayer.Entities
{
    public class PerformanceReviewGoal : DbEntityIdBase
    {
        public Guid PerformanceReviewId { get; set; }
        public virtual PerformanceReview? PerformanceReview { get; set; }
        public int OrderNo { get; set; } = 0;

        public string Goals { get; set; } = string.Empty;
        public decimal Weight { get; set; } = 0M;
        public string Date { get; set; } = string.Empty;
        public string? Measure4 { get; set; } = string.Empty;
        public string? Measure3 { get; set; } = string.Empty;
        public string? Measure2 { get; set; } = string.Empty;
        public string? Measure1 { get; set; } = string.Empty;
    }
}
