using System.Linq;
using System.Threading.Tasks;
using Pms.Core.Database.Abstraction;
using Pms.Core.Filtering;
using Pms.Datalayer.Entities;
using Pms.Shared.Enums;
using Pms.Shared.Exceptions;

namespace Pms.Datalayer.Commands
{
    public interface IUserDeleteCmd : IDbCommand<UserDeleteCmdModel, IdDto> { }

    public class UserDeleteCmd(PmsDbContext context) :
        DbCommandBase<UserDeleteCmdModel, IdDto>(context, context.UserContext),
        IUserDeleteCmd
    {
        private User? _entityRef;

        protected override async Task BuildCommandAsync()
        {
            if (_entityRef != null)
            {
                _entityRef.IsDeleted = true;

                DbContext.Entry(_entityRef).Property(x => x.DateCreated).IsModified = false;

                ConvertDateTimeFieldsToUtc(_entityRef);

                await Task.Run(() => DbContext.UpdateRange(_entityRef!));
                _result.Id = _entityRef!.Id;
            }
        }

        protected override bool ValidateModel()
        {
            var context = DbContext as PmsDbContext;

            _entityRef = context!.Users
                .FirstOrDefault(pr => pr.Id == _cmd.Id);
            if (_entityRef == null)
                throw new DatabaseAccessException(
                    DbErrorCode.ValidationFailed, $"User is not found.");

            return _entityRef != null;
        }

        private void ConvertDateTimeFieldsToUtc(User entity)
        {
            // DateTime fields convertion for DateCreated
            if (entity.DateCreated.Kind == DateTimeKind.Unspecified)
            {
                entity.DateCreated = DateTime.SpecifyKind(entity.DateCreated, DateTimeKind.Utc);
            }

            // DateTime fields convertion for DateUpdated/LastAccess
            //if (entity.DateUpdated != null && entity.DateUpdated.Value.Kind == DateTimeKind.Unspecified)
            //{
            //    entity.DateUpdated = DateTime.SpecifyKind(entity.DateUpdated.Value, DateTimeKind.Utc).ToUniversalTime();
            //}
        }
    }

    public class UserDeleteCmdModel
    {
        public Guid Id { get; set; }
    }
}
