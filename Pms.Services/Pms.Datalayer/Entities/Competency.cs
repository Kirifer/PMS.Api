using Pms.Core.Database.Abstraction;

namespace Pms.Datalayer.Entities
{
    public class PmsCompetency : DbEntityIdBase
    {
        public string Competency { get; set; } = string.Empty;
        public string Level{ get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsActive { get; set; } 
    }
}
