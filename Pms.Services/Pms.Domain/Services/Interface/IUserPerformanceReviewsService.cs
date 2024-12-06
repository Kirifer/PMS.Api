using Pms.Core.Abstraction;
using Pms.Core.Filtering;
using Pms.Models;

namespace Pms.Domain.Services.Interface
{
    public interface IUserPerformanceReviewsService : IEntityService
    {
        Task<Response<List<PmsUserPerformanceReviewDto>>> GetUserPerformanceReviewsAsync(PmsUserPerformanceReviewFilterDto filter);
        Task<Response<IdDto>> CreateUserPerformanceReviewAsync(PmsUserPerformanceReviewCreateDto payload);
    }
}
