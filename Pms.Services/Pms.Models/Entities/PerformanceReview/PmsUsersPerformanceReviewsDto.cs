using Pms.Shared.Models;

namespace Pms.Models
{
    public class PmsUserPerformanceReviewDto : EntityBaseDto
    {
        public Guid UserId { get; set; }
        public Guid PerformanceReviewId { get; set; }
        public string CalibrationComments { get; set; } = string.Empty;
        public DateTime? EmployeeReviewDate { get; set; }
        public DateTime? ManagerReviewDate { get; set; }
    }
}
