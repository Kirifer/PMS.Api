using Microsoft.EntityFrameworkCore;

using Pms.Core.Database.Abstraction;
using Pms.Core.Extensions;
using Pms.Core.Filtering;
using Pms.Models;
using Pms.Shared.Extensions;

namespace Pms.Datalayer.Queries
{
    public interface IUserQuery : IDbQuery<PmsUserDto, UserQueryFilter>
    { }

    public class UserQuery(PmsDbContext dbContext) :
        DbQueryBase<PmsUserDto, UserQueryFilter>(dbContext),
        IUserQuery
    {
        protected override IQueryable<PmsUserDto> BuildQuery()
        {
            var context = DbContext as PmsDbContext;
            var query = context!.Users.AsNoTracking()
                .ConditionalWhere(() => _criteria.IsActive.HasValue,
                    c => c.IsActive == _criteria.IsActive)

                .ConditionalWhere(() => _criteria.Id.HasValue,
                    c => c.Id.Equals(_criteria.Id))
                .ConditionalWhere(() => !_criteria.Ids.IsNullOrEmpty(),
                    c => _criteria.Ids.Contains(c.Id))

                .ConditionalWhereContains(
                    (() => !string.IsNullOrWhiteSpace(_criteria.FirstName),
                        _criteria.FirstName!, c => c.FirstName),
                    (() => !string.IsNullOrWhiteSpace(_criteria.LastName),
                        _criteria.LastName!, c => c.LastName),
                    (() => !string.IsNullOrWhiteSpace(_criteria.Email),
                        _criteria.Email!, c => c.Email));

            return query
                .Select(user => new PmsUserDto()
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Position = user.Position,
                    IsSupervisor = user.IsSupervisor,
                    IsActive = user.IsActive,
                });
        }
    }

    public class UserQueryFilter : FilterBase
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public bool? IsSupervisor {  get; set; }
        public bool? IsActive { get; set; }
    }
}
