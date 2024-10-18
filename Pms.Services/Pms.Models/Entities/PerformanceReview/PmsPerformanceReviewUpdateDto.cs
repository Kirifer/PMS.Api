namespace Pms.Models
{
    public class PmsPerformanceReviewUpdateDto
    {
        public Guid Id { get; set; }

        public string Competency { get; set; } = string.Empty;
        public string Level { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool? IsActive { get; set; }

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
