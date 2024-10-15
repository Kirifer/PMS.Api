using Pms.Core.Database.Abstraction;

namespace Pms.Datalayer.Entities
{
    public class PerformanceReview : DbEntityFullBase
    {
        public string Name { get; set; } = string.Empty;
        public DateOnly? StartYear { get; set; }
        public DateOnly? EndYear { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid? EmployeeId { get; set; }
        public Guid? SupervisorId { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public virtual List<PerformanceReviewGoal> Goals { get; set; } = [];
        public virtual List<PerformanceReviewCompetency> Competencies { get; set; } = [];
    }
}
