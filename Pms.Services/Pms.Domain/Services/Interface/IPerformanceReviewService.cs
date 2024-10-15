using Pms.Core.Abstraction;
using Pms.Core.Filtering;
using Pms.Models;

namespace Pms.Domain.Services.Interface
{
    public interface IPerformanceReviewService : IEntityService
    {
        Task<Response<List<PmsPerformanceReviewDto>>> GetPerformanceReviewsAsync(PmsPerformanceReviewFilterDto filter);
        Task<Response<PmsPerformanceReviewDto>> GetPerformanceReviewAsync(Guid id);
        Task<Response<IdDto>> CreatePerformanceReviewAsync(PmsPerformanceReviewCreateDto payload);
        Task<Response<IdDto>> UpdatePerformanceReviewAsync(Guid id, PmsPerformanceReviewUpdateDto payload);
        Task<Response<IdDto>> DeletePerformanceReviewAsync(Guid id);
    }
}
