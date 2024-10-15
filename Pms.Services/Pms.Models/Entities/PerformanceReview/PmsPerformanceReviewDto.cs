using Pms.Shared.Models;

namespace Pms.Models
{
    public class PmsPerformanceReviewDto : EntityBaseDto
    {
        public string Name { get; set; } = string.Empty;
        public DateOnly? StartYear { get; set; }
        public DateOnly? EndYear { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public Guid? EmployeeId { get; set; }
        public Guid? SupervisorId { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public List<PmsPerformanceReviewGoalDto>? Goals { get; set; }
        public List<PmsPerformanceReviewCompetencyDto>? Competencies { get; set; }
    }
}
