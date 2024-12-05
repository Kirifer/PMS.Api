﻿using Pms.Core.Abstraction;
using Pms.Core.Filtering;
using Pms.Datalayer.Queries;
using Pms.Models;

namespace Pms.Domain.Services.Interface
{
    public interface IUsersPerformanceReviewsService : IEntityService
    {
        Task<Response<List<PmsPerformanceReviewDto>>> GetUserPerformanceReviewsAsync(PmsUserPerformanceReviewFilterDto filter);
        Task<Response<object>> GetUserPerformanceReviewsAsync(UserPerformanceReviewQueryFilter filter);
    }
}
