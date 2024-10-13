using Pms.Core.Filtering;

namespace Pms.Models
{
    public class PmsCompetencyFilterDto : FullFilterBase
    {
        public bool? IsActive { get; set; }
    }
}
