using Pms.Core.Config.Database;

namespace Pms.Core.Config
{
    public class MicroServiceConfig : IMicroServiceConfig
    {
        public DatabaseConfig? DatabaseConfig { get; set; }
    }
}
