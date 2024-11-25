using Pms.Core.Config.Database;
using Pms.ITSquarehub.Authentication.Config;

using Pns.Core.Config.Authentication;

namespace Pms.Domain.Services.Config
{
    public interface IMicroServiceConfig
    {
        DatabaseConfig? DatabaseConfig { get; set; }

        JwtConfig? JwtConfig { get; set; }

        AuthenticationConfig? AuthenticationConfig { get; set; }
    }
}
