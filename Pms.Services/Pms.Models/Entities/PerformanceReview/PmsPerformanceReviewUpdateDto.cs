using Pms.Models.Enums;

namespace Pms.Models
{
    public class PmsPerformanceReviewUpdateDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public DepartmentType DepartmentType { get; set; }
        public int? StartYear { get; set; }
        public int? EndYear { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public Guid? EmployeeId { get; set; }
        public Guid? SupervisorId { get; set; }

        public List<PmsPerformanceReviewGoalUpdateDto>? Goals { get; set; }

        public List<PmsPerformanceReviewCompetencyUpdateDto>? Competencies { get; set; }
    }

    public class PmsPerformanceReviewGoalUpdateDto
    {
        public Guid Id { get; set; }

        public int OrderNo { get; set; }

        public string? Goals { get; set; }
        public decimal Weight { get; set; }
        public string? Date { get; set; }
        public string? Measure4 { get; set; }
        public string? Measure3 { get; set; }
        public string? Measure2 { get; set; }
        public string? Measure1 { get; set; }
    }

    public class PmsPerformanceReviewCompetencyUpdateDto
    {
        public Guid Id { get; set; }

        public Guid CompetencyId { get; set; }
        public int OrderNo { get; set; } = 0;
        public decimal? Weight { get; set; } = 0M;
    }
}
