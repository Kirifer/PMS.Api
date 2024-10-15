using Pms.Shared.Models;

namespace Pms.Models
{
    public class PmsPerformanceReviewUpdateDto : PmsPerformanceReviewCreateDto
    {
        public string Competency { get; set; } = string.Empty;
        public string Level { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool? IsActive { get; set; }
    }
}
