using Pms.Core.Filtering;

namespace Pms.Models.Entities.UserPerformanceReview
{
    public class PmsUserPerformanceReviewFilterDto : FilterBase
    {
        public Guid? UserId { get; set; }
        public Guid? PerformanceReviewId { get; set; }
        public string? CalibrationComments { get; set; }
        public DateOnly? EmployeeReviewDate { get; set; }
        public DateOnly? ManagerReviewDate { get; set; }
    }

}
