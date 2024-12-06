using Pms.Models.Enums;
using Pms.Shared.Models;

namespace Pms.Models
{
    public class PmsPerformanceReviewDto : EntityFullBaseDto
    {
        public string Name { get; set; } = string.Empty;
        public DepartmentType DepartmentType { get; set; }
        public int? StartYear { get; set; }
        public int? EndYear { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
        public PmsUserDto? Employee { get; set; }
        public PmsUserDto? Supervisor { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDeleted { get; set; }

        public List<PmsPerformanceReviewGoalDto>? Goals { get; set; }
        public List<PmsPerformanceReviewCompetencyDto>? Competencies { get; set; }
        public List<PmsUserPerformanceReviewDto>? UserId { get; set; }
    }
}
