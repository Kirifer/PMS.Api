using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Pms.Core.Abstraction;
using Pms.Core.Filtering;
using Pms.Datalayer.Commands;
using Pms.Datalayer.Queries;
using Pms.Domain.Services.Interface;
using Pms.Models;

namespace Pms.Domain.Services
{
    public class UserPerformanceReviewsService(
        IMapper mapper,
        ILogger<UserPerformanceReviewsService> logger,
        IUserPerformanceReviewQuery userPerformanceReviewQuery,
        IUserPerformanceReviewCreateCmd UserPerformanceReviewCreateCmd
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
        public async Task<Response<IdDto>> CreateUserPerformanceReviewAsync(PmsUserPerformanceReviewCreateDto payload)
        {
            try
            {
                var cmdModel = Mapper.Map<UserPerformanceReviewCreateCmdModel>(payload);
                await UserPerformanceReviewCreateCmd.ExecuteAsync(cmdModel);
                var result = UserPerformanceReviewCreateCmd.GetResult();

                return Response<IdDto>.Success(result);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error occurred while creating competencies.");
                return Response<IdDto>.Exception(ex);
            }
        }

    }
}
