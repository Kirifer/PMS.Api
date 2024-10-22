using Pms.Core.Abstraction;
using Pms.Core.Filtering;
using Pms.Shared;

namespace Pms.Domain.Services.Interface
{
    public interface IAccountService : IEntityService
    {
        Task<Response<AuthUserIdentityDto>> GetIdentityAsync();

        Task<Response<AuthLoginDto>> LoginAsync(AuthLoginRequestDto loginRequest);

        Task<Response<AuthIdentityResultDto>> LogoutAsync();
    }
}
