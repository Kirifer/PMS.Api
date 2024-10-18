namespace Pms.Models
{
    public class PmsPerformanceReviewCreateDto
    {
        public string Competency { get; set; } = string.Empty;
        public string Level { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool? IsActive { get; set; }

        public List<PmsPerformanceReviewGoalCreateDto>? Goals { get; set; }
        public List<PmsPerformanceReviewCompetencyCreateDto>? Competencies { get; set; }
    }

    public class PmsPerformanceReviewGoalCreateDto
    {
        public int OrderNo { get; set; }

        public string? Goals { get; set; }
        public decimal Weight { get; set; }
        public string? Date { get; set; }
        public string? Measure4 { get; set; }
        public string? Measure3 { get; set; }
        public string? Measure2 { get; set; }
        public string? Measure1 { get; set; }
    }

    public class PmsPerformanceReviewCompetencyCreateDto
    {
        public Guid CompetencyId { get; set; }
        public int OrderNo { get; set; } = 0;
        public decimal? Weight { get; set; } = 0M;
    }
}
