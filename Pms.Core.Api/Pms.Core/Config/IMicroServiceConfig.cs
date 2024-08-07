using Pms.Core.Config.Database;

namespace Pms.Core.Config
{
    public interface IMicroServiceConfig
    {
        DatabaseConfig? DatabaseConfig { get; set; }
    }
}
