using Pms.Core.Database.Abstraction;
using Pms.Core.Filtering;
using Pms.Datalayer.Entities;
using Pms.Shared.Enums;
using Pms.Shared.Exceptions;

namespace Pms.Datalayer.Commands
{
    public interface IUserCreateCmd :
    IDbCommand<UserCreateCmdModel, IdDto>
    { }

    public class UserCreateCmd(PmsDbContext context) :
        DbCommandBase<UserCreateCmdModel, IdDto>(context, context.UserContext),
        IUserCreateCmd
    {
        protected override async Task BuildCommandAsync()
        {
            if (_cmd.DateCreated == default)
            {
                _cmd.DateCreated = DateTime.UtcNow;
            }

            var createRef = new User()
            {
                FirstName = _cmd.FirstName,
                LastName = _cmd.LastName,
                Email = _cmd.Email,
                Position = _cmd.Position,
                Password = _cmd.Password,
                IsSupervisor = _cmd.IsSupervisor,
                ItsReferenceId = _cmd.ItsReferenceId,
                DateCreated = _cmd.DateCreated,
                IsActive = true
            };

            await DbContext.AddAsync(createRef);

            _result.Id = createRef.Id;
        }

        protected override bool ValidateModel()
        {
            var context = DbContext as PmsDbContext;

            // Validate existing users
            var existingRecord = context!.Users
                .Where(u => u.IsDeleted == false)
                .FirstOrDefault(pr => pr.Email == _cmd.Email);
            if (existingRecord != null)
                throw new DatabaseAccessException(
                    DbErrorCode.ValidationFailed, $"User with email {_cmd.Email} already exists.");
             
            return true;
        }
    }

    public class UserCreateCmdModel
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public bool IsSupervisor { get; set; }
        public Guid? ItsReferenceId { get; set; }
    }
}
