using Pms.Shared.Models;

namespace Pms.Models
{
    public class PmsUsersPerformanceReview : EntityBaseDto
    {
        public PmsCompetencyDto? Competency { get; set; }
        public decimal? Weight { get; set; }
        public int? OrderNo { get; set; }
    }
}
