using Pms.Shared.Enums;
using Pms.Shared.Exceptions;

using Microsoft.EntityFrameworkCore;

namespace Pms.Core.Database.Abstraction
{
    public interface IDbCommand { }

    public interface IDbCommand<TCommand> : IDbCommand<TCommand, TCommand> { }

    public interface IDbCommand<TCommand, TResult> : IDbCommand
    {
        IDbCommand<TCommand, TResult> SetDbUserContext(IDbUserContext userContext);
        Task<IDbCommand<TCommand, TResult>> ExecuteAsync(TCommand command);
        TResult GetResult();
    }

    public abstract class DbCommandBase<TCommand>(
        DbContext dbContext,
        IDbUserContext userContext) : DbCommandBase<TCommand, TCommand>(dbContext, userContext)
    {
    }

    public abstract class DbCommandBase<TCommand, TResult>(
        DbContext dbContext,
        IDbUserContext userContext) : IDbCommand<TCommand, TResult>
    {
        protected DbContext DbContext { get; private set; } = dbContext;
        protected IDbUserContext UserContext { get; private set; } = userContext;

        protected TCommand _cmd;
        protected TResult _result = Activator.CreateInstance<TResult>();
        protected List<string> _errorMessages = [];

        protected abstract Task BuildCommandAsync();
        protected abstract bool ValidateModel();

        public async Task<IDbCommand<TCommand, TResult>> ExecuteAsync(TCommand cmd)
        {
            _cmd = cmd;

            if (ValidateModel())
            {
                await BuildCommandAsync();
                await DbContext.SaveChangesAsync();
            }
            else
            {
                if (_errorMessages.Any())
                    throw new DatabaseAccessException(
                        DbErrorCode.ValidationFailed,
                        _errorMessages.ToArray());

                var genericError = _cmd != null ?
                    @$"Error while mapping and validating model {_cmd.GetType()}" :
                    "Validation failed due to 'null' command";
                _errorMessages.Add(genericError);

                throw new DatabaseAccessException(
                    DbErrorCode.ValidationFailed,
                    genericError);
            }

            return this;
        }

        public IDbCommand<TCommand, TResult> SetDbUserContext(IDbUserContext userContext)
        {
            UserContext = userContext;
            return this;
        }

        public TResult GetResult() => _result;
    }
}
