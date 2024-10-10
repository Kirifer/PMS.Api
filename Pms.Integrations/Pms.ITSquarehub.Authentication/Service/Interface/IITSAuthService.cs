using Pms.ITSquarehub.Authentication.Models;

namespace Pms.ITSquarehub.Authentication.Service
{
    public interface IITSAuthService
    {
        Task<ITSAuthLoginDto?> LoginAsync(string username, string password);
        Task<ITSAuthIdentityDto?> ConfirmIdentity(string authToken);
    }
}
