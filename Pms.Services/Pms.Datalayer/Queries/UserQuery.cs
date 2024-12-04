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
                .ConditionalWhere(() => !_criteria.ShowDeleted,
                    c => !c.IsDeleted)
                .ConditionalWhere(() => _criteria.IsSupervisor.HasValue,
                    c => c.IsSupervisor == _criteria.IsSupervisor)

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
                    Position = user.Position,
                    Email = user.Email,
                    Password = _criteria.ShowPassword ? user.Password : null,
                    IsSupervisor = user.IsSupervisor,
                    IsActive = user.IsActive,
                    IsDeleted = _criteria.ShowDeleted ? user.IsDeleted : null,
                    ItsReferenceId = user.ItsReferenceId,
                    DateCreated = user.DateCreated
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
        public bool ShowPassword { get; set; } = false;
        public bool ShowDeleted { get; set; } = false;
    }
}
