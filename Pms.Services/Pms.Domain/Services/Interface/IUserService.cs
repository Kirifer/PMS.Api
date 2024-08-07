using Pms.Core.Abstraction;
using Pms.Core.Filtering;
using Pms.Models;

namespace Pms.Domain.Services.Interface
{
    public interface IUserService : IEntityService
    {
        Task<Response<List<PmsUserDto>>> GetUsersAsync(PmsUserFilterDto filter);

        Task<Response<PmsUserDto>> GetUserAsync(Guid id);

        Task<Response<PmsUserDto>> CreateUserAsync(PmsUserCreateDto user);

        Task<Response<PmsUserDto>> UpdateUserAsync(Guid id, PmsUserUpdateDto user);

        Task<Response<PmsUserDto>> DeleteUserAsync(Guid id);
    }
}
