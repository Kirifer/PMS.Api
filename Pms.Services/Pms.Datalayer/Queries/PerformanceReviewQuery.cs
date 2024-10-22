using Microsoft.EntityFrameworkCore;

using Pms.Core.Database.Abstraction;
using Pms.Core.Extensions;
using Pms.Core.Filtering;
using Pms.Models;
using Pms.Models.Enums;
using Pms.Shared.Extensions;

namespace Pms.Datalayer.Queries
{
    public interface IPerformanceReviewQuery : IDbQuery<PmsPerformanceReviewDto, PerformanceReviewQueryFilter>
    { }

    public class PerformanceReviewQuery(PmsDbContext dbContext) :
        DbQueryBase<PmsPerformanceReviewDto, PerformanceReviewQueryFilter>(dbContext),
        IPerformanceReviewQuery
    {
        protected override IQueryable<PmsPerformanceReviewDto> BuildQuery()
        {
            var context = DbContext as PmsDbContext;
            var query = context!.PerformanceReviews.AsNoTracking()
                .Include(pr => pr.Goals)
                .Include(pr => pr.Competencies)
                    .ThenInclude(c => c.Competency)

                .ConditionalWhere(() => _criteria.Id.HasValue,
                    c => c.Id.Equals(_criteria.Id))
                .ConditionalWhere(() => !_criteria.Ids.IsNullOrEmpty(),
                    c => _criteria.Ids.Contains(c.Id));

            return query
                .Select(pr => new PmsPerformanceReviewDto()
                {
                    Id = pr.Id,
                    DepartmentType = pr.DepartmentType,
                    Name = pr.Name,
                    EmployeeId = pr.EmployeeId,
                    StartYear = pr.StartYear != null ? pr.StartYear.Value.Year : null,
                    EndYear = pr.EndYear != null ? pr.EndYear.Value.Year : null,
                    StartDate = pr.StartDate,
                    EndDate = pr.EndDate,
                    SupervisorId = pr.SupervisorId,
                    IsActive = pr.IsActive,
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
                });
        }
    }

    public class PerformanceReviewQueryFilter : FilterBase
    {
        public string? Name { get; set; }
        public DateOnly? StartYear { get; set; }
        public DateOnly? EndYear { get; set; }
        public DateOnly? StartDate { get; set; }
        public DateOnly? EndDate { get; set; }
    }
}
