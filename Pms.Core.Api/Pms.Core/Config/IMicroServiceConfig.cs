using Pms.Core.Config.Database;

using Pns.Core.Config.Authentication;

namespace Pms.Core.Config
{
    public interface IMicroServiceConfig
    {
        DatabaseConfig? DatabaseConfig { get; set; }

        JwtConfig? JwtConfig { get; set; }
    }
}
