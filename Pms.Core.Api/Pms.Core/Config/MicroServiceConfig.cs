using Pms.Core.Config.Database;

using Pns.Core.Config.Authentication;

namespace Pms.Core.Config
{
    public class MicroServiceConfig : IMicroServiceConfig
    {
        public DatabaseConfig? DatabaseConfig { get; set; }
        public JwtConfig? JwtConfig { get; set; }

    }
}
