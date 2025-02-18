using Pms.Shared.Models;

namespace Pms.Models.Entities.UserPerformanceReview
{
    public class PmsUserPerformanceReviewGoalDto : EntityFullBaseDto
    {
        public PmsUserPerformanceReviewDto? UserPerformanceReviewId { get; set; }
        public PmsPerformanceReviewGoalDto? PerformanceReviewGoalId { get; set; }
        public int? Value { get; set; }
        public string? Comment { get; set; }
        public bool? IsManager { get; set; }
    }
}