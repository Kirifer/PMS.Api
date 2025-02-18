using Pms.Core.Filtering;

namespace Pms.Models
{
    public class PmsUserPerformanceReviewFilterDto : FilterBase
    {
        public Guid? UserId { get; set; }
        public Guid? PerformanceReviewId { get; set; }
        public string? CalibrationComments { get; set; }
        public DateTime? EmployeeReviewDate { get; set; }
        public DateTime? ManagerReviewDate { get; set; }
    }

}
