using Pms.Core.Filtering;
using Pms.ITSquarehub.Authentication.Models;

namespace Pms.ITSquarehub.Authentication.Service
{
    public interface IITSAuthService
    {
        Task<Response<ITSAuthLoginDto>?> LoginAsync(string username, string password);
        Task<Response<ITSAuthIdentityDto>?> ConfirmIdentity(string authToken);
    }
}
