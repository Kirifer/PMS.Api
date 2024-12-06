using Microsoft.EntityFrameworkCore;

using Pms.Core.Database.Abstraction;
using Pms.Core.Extensions;
using Pms.Core.Filtering;
using Pms.Models;
using Pms.Shared.Extensions;

namespace Pms.Datalayer.Queries
{
    public interface IUserPerformanceReviewQuery : IDbQuery<PmsUserPerformanceReviewDto, UserPerformanceReviewQueryFilter>
    { }

    public class UserPerformanceReviewQuery(PmsDbContext dbContext) :
        DbQueryBase<PmsUserPerformanceReviewDto, UserPerformanceReviewQueryFilter>(dbContext),
        IUserPerformanceReviewQuery
    {
        protected override IQueryable<PmsUserPerformanceReviewDto> BuildQuery()
        {
            var context = DbContext as PmsDbContext;
            var query = context!.UserPerformanceReviews.AsNoTracking()
                .ConditionalWhere(() => _criteria.UserId.HasValue,
                    upr => upr.UserId == _criteria.UserId)
                .ConditionalWhere(() => _criteria.PerformanceReviewId.HasValue,
                    upr => upr.PerformanceReviewId == _criteria.PerformanceReviewId)
                .ConditionalWhereContains(
                    (() => !string.IsNullOrWhiteSpace(_criteria.CalibrationComments),
                        _criteria.CalibrationComments!, upr => upr.CalibrationComments))
                .ConditionalWhere(() => _criteria.EmployeeReviewDate.HasValue,
                    upr => upr.EmployeeReviewDate == _criteria.EmployeeReviewDate)
                .ConditionalWhere(() => _criteria.ManagerReviewDate.HasValue,
                    upr => upr.ManagerReviewDate == _criteria.ManagerReviewDate);

            return query
                .Select(upr => new PmsUserPerformanceReviewDto()
                {
                    Id = upr.Id,
                    UserId = upr.UserId,
                    PerformanceReviewId = upr.PerformanceReviewId,
                    CalibrationComments = upr.CalibrationComments,
                    EmployeeReviewDate = upr.EmployeeReviewDate,
                    ManagerReviewDate = upr.ManagerReviewDate
                });
        }
    }

    public class UserPerformanceReviewQueryFilter : FilterBase
    {
        public Guid? UserId { get; set; }
        public Guid? PerformanceReviewId { get; set; }
        public string? CalibrationComments { get; set; }
        public DateTime? EmployeeReviewDate { get; set; }
        public DateTime? ManagerReviewDate { get; set; }
    }
}
