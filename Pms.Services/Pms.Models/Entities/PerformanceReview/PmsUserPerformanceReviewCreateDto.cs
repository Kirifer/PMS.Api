namespace Pms.Models
{
    public class PmsUserPerformanceReviewCreateDto
    {
        public string? CallibrationComments { get; set; }
        public DateOnly? EmployeeReviewDate { get; set; }
        public DateOnly? ManagerReviewDate { get; set; }
        public Guid? UserId { get; set; }
        public Guid? PerformanceReviewId { get; set; }
        public List<PmsUserPerfromanceReviewGoalsCreateDto>? Goals { get; set; }
        public List<PmsUserPeformanceReviewCompetenciesCreateDto>? Competencies { get; set; }
    }
    public class PmsUserPerfromanceReviewGoalsCreateDto
    {
     
        public Guid PerformanceReviewGoalId { get; set; }
        public int? Value { get; set; }
        public string? Comment { get; set; }
        public bool? IsManager {  get; set; }
    }
    public class PmsUserPeformanceReviewCompetenciesCreateDto

    {
        public Guid PerformanceReviewCompetencyId { get; set; }
        public int? Value { get; set; }
        public string? Comment { get; set; }
        public bool? IsManager { get; set; }
    }
}
