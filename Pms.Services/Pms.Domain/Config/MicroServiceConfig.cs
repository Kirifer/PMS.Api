using Pms.Core.Config.Database;
using Pms.ITSquarehub.Authentication.Config;

using Pns.Core.Config.Authentication;

namespace Pms.Domain.Services.Config
{
    public class MicroServiceConfig : IMicroServiceConfig
    {
        public DatabaseConfig? DatabaseConfig { get; set; }
        public JwtConfig? JwtConfig { get; set; }
        public AuthenticationConfig? AuthenticationConfig { get; set; }
    }
}
