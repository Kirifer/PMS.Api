using AutoMapper;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

using Pms.Core.Abstraction;
using Pms.Core.Filtering;
using Pms.Datalayer.Commands;
using Pms.Datalayer.Queries;
using Pms.Domain.Services.Interface;
using Pms.Models;

namespace Pms.Domain.Services
{
    public class UserService (IMapper mapper,
            ILogger<UserService> logger,
            IUserQuery userQuery,
            IUserCreateCmd userCreateCmd,
            IUserUpdateCmd userUpdateCmd,
            IUserDeleteCmd userDeleteCmd) : EntityService(mapper, logger), IUserService
    {
        public async Task<Response<List<PmsUserDto>>> GetUsersAsync(PmsUserFilterDto filter)
        {
            try
            { 
                var queryFilter = Mapper.Map<UserQueryFilter>(filter);
                var result = await userQuery
                    .GetQuery(queryFilter)
                    .ToListAsync();
                var totalCount = userQuery.GetTotalCount();

                return Response<List<PmsUserDto>>.Success(result, totalCount);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error occurred while fetching users");
                return Response<List<PmsUserDto>>.Exception(ex);
            }
        }

        public async Task<Response<PmsUserDto>> GetUserAsync(Guid id)
        {
            try
            {
                var result = await userQuery
                    .GetQuery(new UserQueryFilter { Id = id })
                    .FirstOrDefaultAsync();
                if (result == null)
                {
                    return Response<PmsUserDto>.Error(System.Net.HttpStatusCode.NotFound,
                        new Shared.ErrorDto(Shared.Enums.ErrorCode.NoRecordFound, "Record Not Found."));
                }

                return Response<PmsUserDto>.Success(result);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error occurred while fetching user with id.");
                return Response<PmsUserDto>.Exception(ex);
            }
        }

        public async Task<Response<IdDto>> CreateUserAsync(PmsUserCreateDto payload)
        {
            try
            {
                var cmdModel = Mapper.Map<UserCreateCmdModel>(payload);
                await userCreateCmd.ExecuteAsync(cmdModel);
                var result = userCreateCmd.GetResult();

                return Response<IdDto>.Success(result);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error occurred while creating user.");
                return Response<IdDto>.Exception(ex);
            }
        }

        public async Task<Response<IdDto>> UpdateUserAsync(Guid id, PmsUserUpdateDto payload)
        {
            try
            {
                var cmdModel = Mapper.Map<UserUpdateCmdModel>(payload);
                cmdModel.Id = id;
                await userUpdateCmd.ExecuteAsync(cmdModel);
                var result = userUpdateCmd.GetResult();

                return Response<IdDto>.Success(result);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error occurred while updating user.");
                return Response<IdDto>.Exception(ex);
            }
        }

        public async Task<Response<IdDto>> DeleteUserAsync(Guid id)
        {
            try
            {
                await userDeleteCmd.ExecuteAsync(new UserDeleteCmdModel { Id = id });
                var result = userDeleteCmd.GetResult();

                return Response<IdDto>.Success(result);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error occurred while deleting user.");
                return Response<IdDto>.Exception(ex);
            }
        }
    }
}
