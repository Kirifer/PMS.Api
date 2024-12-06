using Pms.Shared.Models;

namespace Pms.Models
{
    public class PmsUserPerformanceReviewDto : EntityFullBaseDto
    {
        public Guid UserId { get; set; }
        public Guid PerformanceReviewId { get; set; }
        public string CalibrationComments { get; set; } = string.Empty;
        public DateOnly? EmployeeReviewDate { get; set; }
        public DateOnly? ManagerReviewDate { get; set; }
    }
}
