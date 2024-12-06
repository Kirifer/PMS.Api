using Pms.Core.Database.Abstraction;

namespace Pms.Datalayer.Entities
{
    public class PmsUserPerformanceReview : DbEntityFullBase
    {
        public Guid UserId { get; set; }
        public Guid PerformanceReviewId { get; set; }
        public string CalibrationComments { get; set; } = string.Empty;
        public DateTime? EmployeeReviewDate { get; set; }
        public DateTime? ManagerReviewDate { get; set; }
    }
}
