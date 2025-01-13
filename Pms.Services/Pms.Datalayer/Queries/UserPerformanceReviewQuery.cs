using Microsoft.EntityFrameworkCore;

using Pms.Core.Database.Abstraction;
using Pms.Core.Extensions;
using Pms.Core.Filtering;
using Pms.Models;
using Pms.Models.Entities.UserPerformanceReview;
using Pms.Shared.Extensions;

namespace Pms.Datalayer.Queries
{
    public interface IUserPerformanceReviewQuery : IDbQuery<PmsUserPerformanceReviewDto, UserPerformanceReviewQueryFilter>
    { }

    public class UserPerformanceReviewQuery(PmsDbContext dbContext) :
        DbQueryBase<PmsUserPerformanceReviewDto,
            UserPerformanceReviewQueryFilter>(dbContext),
        IUserPerformanceReviewQuery
    {
        protected override IQueryable<PmsUserPerformanceReviewDto> BuildQuery()
        {
            var context = DbContext as PmsDbContext;
            var query = context!.UserPerformanceReviews.AsNoTracking()
                .Include(upr => upr.Goals)
                .Include(upr => upr.Competencies)
                .ConditionalWhere(() => _criteria.UserId.HasValue,
                    upr => upr.UserId == _criteria.UserId);

            return query
                .Select(upr => new PmsUserPerformanceReviewDto
                {
                    Id = upr.Id,
                    User = upr.UserId.HasValue ? context.Users.Where(u => u.Id == upr.UserId).Select(u => new PmsUserDto()
                    {
                        Id = u.Id,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Email = u.Email,
                        Position = u.Position,
                    }).FirstOrDefault() : null,

                    //GoalComments = upr.Goals != null ? upr.Goals.Select(g => new PmsUserPerformanceReviewGoalDto
                    //{
                    //    Id = g.Id,
                    //    Value = g.Value,
                    //    Comment = g.Comment,
                    //    IsManager = g.IsManager
                    //}).ToList() : null,

                    //CompetencyComments = upr.Competencies != null ? upr.Competencies.Select(g => new PmsUserPerformanceReviewCompetencyDto
                    //{
                    //    Id = g.Id,
                    //    Value = g.Value,
                    //    Comment = g.Comment,
                    //    IsManager = g.IsManager
                    //}).ToList() : null,

                    PerformanceReview = upr.PerformanceReviewId.HasValue ? context.PerformanceReviews.Where(pr => pr.Id == upr.PerformanceReviewId).Select(pr => new PmsPerformanceReviewDto()
                    {
                        Id = pr.Id,
                        Name = pr.Name,
                        StartDate = pr.StartDate,
                        EndDate = pr.EndDate,
                        IsActive = pr.IsActive,
                        DepartmentType = pr.DepartmentType,
                        Competencies = pr.Competencies != null ? pr.Competencies.OrderBy(c => c.OrderNo).Select(c => new PmsPerformanceReviewCompetencyDto
                        {
                            Id = c.Id,
                            Competency = new PmsCompetencyDto
                            {
                                Id = c.CompetencyLevelId,
                                Competency = c.Competency != null ? c.Competency.Competency : string.Empty,
                                Level = c.Competency != null ? c.Competency.Level : string.Empty,
                                Description = c.Competency != null ? c.Competency.Description : string.Empty,
                            },
                            OrderNo = c.OrderNo,
                            Weight = c.Weight
                        }).ToList() : null,
                        Goals = pr.Goals != null ? pr.Goals.OrderBy(c => c.OrderNo).Select(c => new PmsPerformanceReviewGoalDto
                        {
                            Id = c.Id,
                            OrderNo = c.OrderNo,
                            Goals = c.Goals,
                            Weight = c.Weight,
                            Date = c.Date,
                            Measure1 = c.Measure1,
                            Measure2 = c.Measure2,
                            Measure3 = c.Measure3,
                            Measure4 = c.Measure4
                        }).ToList() : null,
                    }).FirstOrDefault() : null,

                    CalibrationComments = upr.CalibrationComments,
                    EmployeeReviewDate = upr.EmployeeReviewDate,
                    ManagerReviewDate = upr.ManagerReviewDate,
                    CreatedOn = upr.CreatedOn
                });
        }

    }

    public class UserPerformanceReviewQueryFilter : FilterBase
    {
        public Guid? UserId { get; set; }
        public Guid? PerformanceReviewId { get; set; }
        public string? CalibrationComments { get; set; }
        public DateOnly? EmployeeReviewDate { get; set; }
        public DateOnly? ManagerReviewDate { get; set; }
    }
}
