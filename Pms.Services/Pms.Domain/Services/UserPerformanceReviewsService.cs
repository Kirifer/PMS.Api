using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Pms.Core.Abstraction;
using Pms.Core.Filtering;
using Pms.Datalayer.Queries;
using Pms.Domain.Services.Interface;
using Pms.Models;

namespace Pms.Domain.Services
{
    public class UserPerformanceReviewsService(
        IMapper mapper,
        ILogger<UserPerformanceReviewsService> logger,
        IUserPerformanceReviewQuery userPerformanceReviewQuery
    ) : EntityService(mapper, logger), IUserPerformanceReviewsService
    {
        public async Task<Response<List<PmsUserPerformanceReviewDto>>> GetUserPerformanceReviewsAsync(PmsUserPerformanceReviewFilterDto filter)
        {
            try
            {
                var queryFilter = Mapper.Map<UserPerformanceReviewQueryFilter>(filter);
                var result = await userPerformanceReviewQuery
                    .GetQuery(queryFilter)
                    .ToListAsync();
                var totalCount = userPerformanceReviewQuery.GetTotalCount();

                return Response<List<PmsUserPerformanceReviewDto>>.Success(result, totalCount);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error occurred while fetching user performance reviews");
                return Response<List<PmsUserPerformanceReviewDto>>.Exception(ex);
            }
        }

        //public Task<Response<object>> GetUserPerformanceReviewsAsync(UserPerformanceReviewQueryFilter filter)
        //{
        //    throw new NotImplementedException();
        //}

        //Task<Response<List<PmsUserPerformanceReviewDto>>> IUserPerformanceReviewsService.GetUserPerformanceReviewsAsync(PmsUserPerformanceReviewFilterDto filter)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
