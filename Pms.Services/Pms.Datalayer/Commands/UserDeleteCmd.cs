using Pms.Core.Database.Abstraction;
using Pms.Core.Filtering;
using Pms.Datalayer.Entities;
using Pms.Shared.Enums;
using Pms.Shared.Exceptions;

namespace Pms.Datalayer.Commands
{
    public interface IUserDeleteCmd :
    IDbCommand<UserDeleteCmdModel, IdDto>
    { }

    public class UserDeleteCmd(PmsDbContext context) :
        DbCommandBase<UserDeleteCmdModel, IdDto>(context, context.UserContext),
        IUserDeleteCmd
    {
        private User? _entityRef;

        protected override async Task BuildCommandAsync()
        {
            _entityRef!.IsDeleted = true;

            await Task.Run(() => DbContext.UpdateRange(_entityRef!));
            _result.Id = _entityRef!.Id;
        }

        protected override bool ValidateModel()
        {
            var context = DbContext as PmsDbContext;

            // Validate existing users
            _entityRef = context!.Users
                .FirstOrDefault(pr => pr.Id == _cmd.Id);
            if (_entityRef == null)
                throw new DatabaseAccessException(
                    DbErrorCode.ValidationFailed, $"User is not found.");

            return _entityRef != null;
        }
    }

    public class UserDeleteCmdModel
    {
        public Guid Id { get; set; }
    }
}
