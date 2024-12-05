using Pms.Core.Database.Abstraction;
using Pms.Models.Enums;

namespace Pms.Datalayer.Entities
{
    public class PerformanceReview : DbEntityFullBase
    {
        public string Name { get; set; } = string.Empty;
        public DepartmentType DepartmentType { get; set; }
        public DateOnly? StartYear { get; set; }
        public DateOnly? EndYear { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public Guid? EmployeeId { get; set; }
        public Guid? SupervisorId { get; set; }
        public bool IsActive { get; set; } = true;
        public bool IsDeleted { get; set; } = false;

        public DateTimeOffset? CreatedOn { get; set; }

        public virtual List<PerformanceReviewGoal> Goals { get; set; } = [];
        public virtual List<PerformanceReviewCompetency> Competencies { get; set; } = [];
    }
}
