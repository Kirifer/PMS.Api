using Pms.Shared.Models;

namespace Pms.Models
{
    public class PmsUserPerformanceReviewDto : EntityFullBaseDto
    {
        public string CalibrationComments { get; set; } = string.Empty;
        public DateOnly? EmployeeReviewDate { get; set; }
        public DateOnly? ManagerReviewDate { get; set; }
        public PmsUserDto? User { get; set; }
        public PmsPerformanceReviewDto? PerformanceReview { get; set; }
    }
}
