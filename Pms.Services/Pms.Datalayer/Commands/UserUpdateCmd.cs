using Pms.Core.Database.Abstraction;
using Pms.Core.Filtering;
using Pms.Datalayer.Entities;
using Pms.Shared.Enums;
using Pms.Shared.Exceptions;

namespace Pms.Datalayer.Commands
{
    public interface IUserUpdateCmd :
    IDbCommand<UserUpdateCmdModel, IdDto>
    { }

    public class UserUpdateCmd(PmsDbContext context) :
        DbCommandBase<UserUpdateCmdModel, IdDto>(context, context.UserContext),
        IUserUpdateCmd
    {
        private User? _updateRef;

        protected override async Task BuildCommandAsync()
        {

            _updateRef.FirstName = _cmd.FirstName;
            _updateRef.LastName = _cmd.LastName;
            _updateRef.Email = _cmd.Email;
            _updateRef.Password = _cmd.Password;
            _updateRef.Position = _cmd.Position;
            _updateRef.IsSupervisor = _cmd.IsSupervisor;
            _updateRef!.DateCreated = _cmd.DateCreated.ToUniversalTime();

            if (_cmd.IsActive.HasValue)
                _updateRef.IsActive = _cmd.IsActive.Value;

            _updateRef.ItsReferenceId = _cmd.ItsReferenceId;

            await Task.Run(() => context.UpdateRange(_updateRef));

            _result.Id = _updateRef.Id;
        }


        protected override bool ValidateModel()
        {
            var context = DbContext as PmsDbContext;

            _updateRef = context!.Users
                .FirstOrDefault(pr => pr.Id == _cmd.Id);
            if (_updateRef == null)
                throw new DatabaseAccessException(
                    DbErrorCode.ValidationFailed, $"User is not found.");

            var existSameEmail = context.Users
                .Where(u => u.IsDeleted == false)
                .Where(u => u.Id != _updateRef.Id && u.Email == _cmd.Email)
                .Any();
            if (existSameEmail)
            {
                throw new DatabaseAccessException(
                    DbErrorCode.ValidationFailed, $"Same email address user exists. Kindly check.");
            }

            return _updateRef != null;
        }
    }

    public class UserUpdateCmdModel
    {

        public Guid Id { get; set; }

        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public bool IsSupervisor { get; set; }
        public bool? IsActive { get; set; }
        public Guid? ItsReferenceId { get; set; }
        public DateTime DateCreated { get;  } = DateTime.UtcNow;
    }
}
