using Pms.Core.Abstraction;
using Pms.Core.Filtering;
using Pms.Datalayer.Queries;
using Pms.Models;

namespace Pms.Domain.Services.Interface
{
    public interface IUserPerformanceReviewsService : IEntityService
    {
        Task<Response<List<PmsPerformanceReviewDto>>> GetUserPerformanceReviewsAsync(PmsUserPerformanceReviewFilterDto filter);
        Task<Response<object>> GetUserPerformanceReviewsAsync(UserPerformanceReviewQueryFilter filter);
        Task<Response<IdDto>> CreateUserPerformanceReviewAsync(PmsUserPerformanceReviewCreateDto payload);
    }
}
