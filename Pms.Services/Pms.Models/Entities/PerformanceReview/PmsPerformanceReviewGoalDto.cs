using Pms.Shared.Models;

namespace Pms.Models
{
    public class PmsPerformanceReviewGoalDto : EntityBaseDto
    {
        public int? OrderNo { get; set; }
        public string? Goals { get; set; }
        public decimal? Weight { get; set; }
        public string? Date { get; set; }
        public string? Measure4 { get; set; }
        public string? Measure3 { get; set; }
        public string? Measure2 { get; set; }
        public string? Measure1 { get; set; }
    }
}
