using Pms.Shared.Models;

namespace Pms.Models.Entities.UserPerformanceReview
{
    public class PmsUserPerformanceReviewDto : EntityFullBaseDto
    {
        public PmsUserDto? User { get; set; }
        public PmsUserDto? Supervisor { get; set; }
        public PmsPerformanceReviewDto? PerformanceReview { get; set; }
        public string? CalibrationComments { get; set; }
        public DateOnly? EmployeeReviewDate { get; set; }
        public DateOnly? ManagerReviewDate { get; set; }
    }
}
