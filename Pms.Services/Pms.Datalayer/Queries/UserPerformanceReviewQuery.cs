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

            var queryWithDetails = query
                .Join(context.Users, upr => upr.UserId, user => user.Id, (upr, user) => new { upr, user })
                .Join(context.PerformanceReviews, result => result.upr.PerformanceReviewId, pr => pr.Id,
                    (result, pr) => new { result.upr, result.user, pr })
                .Select(result => new PmsUserPerformanceReviewDto
                {
                    Id = result.upr.Id,
                    //UserId = result.upr.UserId,
                    User = new PmsUserDto
                    {
                        Id = result.user.Id,
                        FirstName = result.user.FirstName,
                        LastName = result.user.LastName,
                        Position = result.user.Position,
                        Email = result.user.Email,
                        IsSupervisor = result.user.IsSupervisor,
                        IsActive = result.user.IsActive,
                        IsDeleted = result.user.IsDeleted,
                        CreatedOn = result.user.CreatedOn
                    },
                    //PerformanceReviewId = result.upr.PerformanceReviewId,
                    PerformanceReview = new PmsPerformanceReviewDto
                    {
                        Id = result.pr.Id,
                        Name = result.pr.Name,
                        StartDate = result.pr.StartDate,
                        EndDate = result.pr.EndDate,
                        IsActive = result.pr.IsActive,
                        DepartmentType = result.pr.DepartmentType,
                        CreatedOn = result.pr.CreatedOn
                    },
                    CalibrationComments = result.upr.CalibrationComments,
                    EmployeeReviewDate = result.upr.EmployeeReviewDate,
                    ManagerReviewDate = result.upr.ManagerReviewDate,
                    CreatedOn = result.upr.CreatedOn
                });

            return queryWithDetails;
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
