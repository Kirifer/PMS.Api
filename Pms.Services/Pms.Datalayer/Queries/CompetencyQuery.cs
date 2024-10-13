using Microsoft.EntityFrameworkCore;

using Pms.Core.Database.Abstraction;
using Pms.Core.Extensions;
using Pms.Core.Filtering;
using Pms.Models;
using Pms.Shared.Extensions;

namespace Pms.Datalayer.Queries
{
    public interface ICompetencyQuery : IDbQuery<PmsCompetencyDto, CompetencyQueryFilter>
    { }

    public class CompetencyQuery(PmsDbContext dbContext) :
        DbQueryBase<PmsCompetencyDto, CompetencyQueryFilter>(dbContext),
        ICompetencyQuery
    {
        protected override IQueryable<PmsCompetencyDto> BuildQuery()
        {
            var context = DbContext as PmsDbContext;
            var query = context!.Competencies.AsNoTracking()
                .ConditionalWhere(() => _criteria.IsActive.HasValue,
                    c => c.IsActive == _criteria.IsActive)

                .ConditionalWhere(() => _criteria.Id.HasValue,
                    c => c.Id.Equals(_criteria.Id))
                .ConditionalWhere(() => !_criteria.Ids.IsNullOrEmpty(),
                    c => _criteria.Ids.Contains(c.Id))

                .ConditionalWhereContains(
                    (() => !string.IsNullOrWhiteSpace(_criteria.Competency),
                        _criteria.Competency, c => c.Competency),
                    (() => !string.IsNullOrWhiteSpace(_criteria.Level),
                        _criteria.Level, c => c.Level));

            return query
                .Select(project => new PmsCompetencyDto()
                {
                    Id = project.Id,
                    Competency = project.Competency,
                    Level = project.Level,
                    Description = project.Description,
                });
        }
    }

    public class CompetencyQueryFilter : FilterBase
    {
        public string Competency {  get; set; } = string.Empty;
        public string Level { get; set; } = string.Empty;
        public bool? IsActive { get; set; }
    }
}
