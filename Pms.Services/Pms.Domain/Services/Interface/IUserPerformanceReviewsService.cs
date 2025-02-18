using Pms.Core.Abstraction;
using Pms.Core.Filtering;
using Pms.Models.Entities.UserPerformanceReview;

namespace Pms.Domain.Services.Interface
{
    public interface IUserPerformanceReviewsService : IEntityService
    {
        Task<Response<List<PmsUserPerformanceReviewDto>>> GetUserPerformanceReviewsAsync(Guid id);
        Task<Response<IdDto>> CreateUserPerformanceReviewAsync(PmsUserPerformanceReviewCreateDto payload);
    }
}
