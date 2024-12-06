using Pms.Core.Database.Abstraction;

namespace Pms.Datalayer.Entities
{
    public class UserPerformanceReview : DbEntityFullBase
    {
        public Guid UserId { get; set; }
        public Guid PerformanceReviewId { get; set; }
        public string CalibrationComments { get; set; } = string.Empty;
        public DateOnly? EmployeeReviewDate { get; set; }
        public DateOnly? ManagerReviewDate { get; set; }
        public virtual List<UserPerformanceReviewGoals> Goals { get; set; } = [];
        public virtual List<UserPerformanceReviewCompetencies> Competencies { get; set; } = [];
    }
}
