//using AutoMapper;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Logging;
//using Pms.Core.Abstraction;
//using Pms.Core.Filtering;
//using Pms.Datalayer.Queries;
//using Pms.Domain.Services.Interface;
//using Pms.Models;

//namespace Pms.Domain.Services
//{
//    public class UserPerformanceReviewsService(
//        IMapper mapper,
//        ILogger<UserPerformanceReviewsService> logger,
//        IUserPerformanceReviewQuery userPerformanceReviewQuery
//    ) : EntityService(mapper, logger), IUserPerformanceReviewsService
//    {
//        private readonly IUserPerformanceReviewQuery _userPerformanceReviewQuery;

//        public UserPerformanceReviewsService(
//            IMapper mapper,
//            ILogger<UserPerformanceReviewsService> logger,
//            IUserPerformanceReviewQuery userPerformanceReviewQuery
//        ) : base(mapper, logger)
//        {
//            _userPerformanceReviewQuery = userPerformanceReviewQuery;
//        }

//        public async Task<Response<List<PmsPerformanceReviewDto>>> GetUserPerformanceReviewsAsync(PmsUserPerformanceReviewFilterDto filter)
//        {
//            try
//            {
//                // Map the filter DTO to the query filter
//                var queryFilter = Mapper.Map<UserPerformanceReviewQueryFilter>(filter);

//                // Execute the query and retrieve results
//                var results = await _userPerformanceReviewQuery
//                    .GetQuery(queryFilter)
//                    .ToListAsync();

//                // Get the total count for pagination purposes (if needed)
//                var totalCount = _userPerformanceReviewQuery.GetTotalCount();

//                // Map the results to DTOs and return success response
//                return Response<List<PmsPerformanceReviewDto>>.Success(results, totalCount);
//            }
//            catch (Exception ex)
//            {
//                // Log the error and return an exception response
//                Logger.LogError(ex, "Error occurred while fetching user performance reviews");
//                return Response<List<PmsPerformanceReviewDto>>.Exception(ex);
//            }
//        }

//        public async Task<Response<object>> GetUserPerformanceReviewsAsync(UserPerformanceReviewQueryFilter filter)
//        {
//            try
//            {
//                // Execute the query with the given filter
//                var results = await _userPerformanceReviewQuery
//                    .GetQuery(filter)
//                    .ToListAsync();

//                // Return the raw object results as a success response
//                return Response<object>.Success(results);
//            }
//            catch (Exception ex)
//            {
//                // Log the error and return an exception response
//                Logger.LogError(ex, "Error occurred while fetching user performance reviews");
//                return Response<object>.Exception(ex);
//            }
//        }
//    }
//}
