using Pms.Core.Filtering;

namespace Pms.Models
{
    public class PmsPerformanceReviewFilterDto : FullFilterBase
    {
        public string? Name { get; set; }
        public DateOnly? StartYear { get; set; }
        public DateOnly? EndYear { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
