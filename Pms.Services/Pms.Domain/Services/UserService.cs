using AutoMapper;

using Microsoft.Extensions.Logging;

using Pms.Core.Abstraction;
using Pms.Core.Filtering;
using Pms.Datalayer.Entities;
using Pms.Datalayer.Interface;
using Pms.Domain.Services.Interface;
using Pms.Models;

namespace Pms.Domain.Services
{
    public class UserService: EntityService, IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(
            IMapper mapper,
            ILogger<UserService> logger,

            IUserRepository userRepository)
            : base(mapper, logger)
        {
            _userRepository = userRepository;
        }

        public async Task<Response<List<PmsUserDto>>> GetUsersAsync(PmsUserFilterDto filter)
        {
            try
            {
                var result = await _userRepository.GetAllAsync(u =>
                string.IsNullOrEmpty(filter.Email) || u.Email == filter.Email);

                var userDtos = Mapper.Map<List<PmsUserDto>>(result);

                return Response<List<PmsUserDto>>.Success(userDtos);
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
                var result = await _userRepository.GetAsync(id);
                var userDto = Mapper.Map<PmsUserDto>(result);
                return Response<PmsUserDto>.Success(userDto);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error occurred while fetching user with id.");
                return Response<PmsUserDto>.Exception(ex);
            }
        }

        public async Task<Response<PmsUserDto>> CreateUserAsync(PmsUserCreateDto user)
        {
            try
            {
                var createRef = Mapper.Map<User>(user);

                var result = await _userRepository.AddAsync(createRef);

                var userDto = Mapper.Map<PmsUserDto>(result);

                return Response<PmsUserDto>.Success(userDto);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error occurred while creating user.");
                return Response<PmsUserDto>.Exception(ex);
            }
        }

        public async Task<Response<PmsUserDto>> UpdateUserAsync(Guid id, PmsUserUpdateDto user)
        {
            try
            {
                var updateRef = await _userRepository.GetAsync(id);

                updateRef.FirstName = user.FirstName;
                updateRef.LastName = user.LastName;
                //updateRef.Email = user.Email;

                var result = await _userRepository.UpdateAsync(updateRef);

                var userDto = Mapper.Map<PmsUserDto>(result);

                return Response<PmsUserDto>.Success(userDto);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error occurred while updating user.");
                return Response<PmsUserDto>.Exception(ex);
            }
        }

        public async Task<Response<PmsUserDto>> DeleteUserAsync(Guid id)
        {
            try
            {
                var deleteRef = await _userRepository.GetAsync(id);

                var result = await _userRepository.DeleteAsync(deleteRef);

                var userDto = Mapper.Map<PmsUserDto>(result);

                return Response<PmsUserDto>.Success(userDto);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error occurred while deleting user.");
                return Response<PmsUserDto>.Exception(ex);
            }
        }
    }
}
