using Pms.Core.Database.Abstraction;
using Pms.Core.Filtering;
using Pms.Datalayer.Entities;
using Pms.Shared.Enums;
using Pms.Shared.Exceptions;

namespace Pms.Datalayer.Commands
{
    public interface IPerformanceReviewDeleteCmd :
    IDbCommand<PerformanceReviewDeleteCmdModel, IdDto>
    { }

    public class PerformanceReviewDeleteCmd(PmsDbContext context) :
        DbCommandBase<PerformanceReviewDeleteCmdModel, IdDto>(context, context.UserContext),
        IPerformanceReviewDeleteCmd
    {
        private PerformanceReview? _entityRef;

        protected override async Task BuildCommandAsync()
        {
            await Task.Run(() => DbContext.Remove(_entityRef!));
            _result.Id = _entityRef!.Id;
        }

        protected override bool ValidateModel()
        {
            var context = DbContext as PmsDbContext;

            // Validate existing project codes
            _entityRef = context!.PerformanceReviews
                .FirstOrDefault(pr => pr.Id == _cmd.PerformanceReviewId);
            if (_entityRef == null)
                throw new DatabaseAccessException(
                    DbErrorCode.ValidationFailed, $"Performance Review is not found.");

            return _entityRef != null;
        }
    }

    public class PerformanceReviewDeleteCmdModel
    {
        public Guid PerformanceReviewId { get; set; }
    }
}
