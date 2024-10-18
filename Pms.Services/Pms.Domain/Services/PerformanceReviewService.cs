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
    public class PerformanceReviewService(IMapper mapper, ILogger<PerformanceReviewService> logger,

        IPerformanceReviewQuery performanceReviewQuery,
        IPerformanceReviewCreateCmd performanceReviewCreateCmd,
        IPerformanceReviewUpdateCmd performanceReviewUpdateCmd,
        IPerformanceReviewDeleteCmd performanceReviewDeleteCmd)
        : EntityService(mapper, logger), IPerformanceReviewService
    {
        public async Task<Response<List<PmsPerformanceReviewDto>>> GetPerformanceReviewsAsync(PmsPerformanceReviewFilterDto filter)
        {
            try
            {
                var queryFilter = Mapper.Map<PerformanceReviewQueryFilter>(filter);
                var result = await performanceReviewQuery
                    .GetQuery(queryFilter)
                    .ToListAsync();
                var totalCount = performanceReviewQuery.GetTotalCount();

                return Response<List<PmsPerformanceReviewDto>>.Success(result, totalCount);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error occurred while fetching performance reviews");
                return Response<List<PmsPerformanceReviewDto>>.Exception(ex);
            }
        }

        public async Task<Response<PmsPerformanceReviewDto>> GetPerformanceReviewAsync(Guid id)
        {
            try
            {
                var result = await performanceReviewQuery
                    .GetQuery(new PerformanceReviewQueryFilter { Id = id })
                    .FirstOrDefaultAsync();
                if (result == null)
                {
                    return Response<PmsPerformanceReviewDto>.Error(System.Net.HttpStatusCode.NotFound,
                        new Shared.ErrorDto(Shared.Enums.ErrorCode.NoRecordFound, "Record Not Found."));
                }

                return Response<PmsPerformanceReviewDto>.Success(result);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error occurred while fetching competencies.");
                return Response<PmsPerformanceReviewDto>.Exception(ex);
            }
        }

        public async Task<Response<IdDto>> CreatePerformanceReviewAsync(PmsPerformanceReviewCreateDto payload)
        {
            try
            {
                var cmdModel = Mapper.Map<PerformanceReviewCreateCmdModel>(payload);
                await performanceReviewCreateCmd.ExecuteAsync(cmdModel);
                var result = performanceReviewCreateCmd.GetResult();

                return Response<IdDto>.Success(result);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error occurred while creating competencies.");
                return Response<IdDto>.Exception(ex);
            }
        }

        public async Task<Response<IdDto>> UpdatePerformanceReviewAsync(Guid id, PmsPerformanceReviewUpdateDto payload)
        {
            try
            {
                var cmdModel = Mapper.Map<PerformanceReviewUpdateCmdModel>(payload);
                await performanceReviewUpdateCmd.ExecuteAsync(cmdModel);
                var result = performanceReviewUpdateCmd.GetResult();

                return Response<IdDto>.Success(result);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error occurred while updating competencies.");
                return Response<IdDto>.Exception(ex);
            }
        }

        public async Task<Response<IdDto>> DeletePerformanceReviewAsync(Guid id)
        {
            try
            {
                await performanceReviewDeleteCmd.ExecuteAsync(new PerformanceReviewDeleteCmdModel { PerformanceReviewId = id});
                var result = performanceReviewDeleteCmd.GetResult();

                return Response<IdDto>.Success(result);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error occurred while creating competencies");
                return Response<IdDto>.Exception(ex);
            }
        }

    }
}
