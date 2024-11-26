
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using AutoMapper;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

using Pms.Core.Abstraction;
using Pms.Core.Authentication;
using Pms.Core.Filtering;
using Pms.Datalayer.Entities;
using Pms.Datalayer.Queries;
using Pms.Domain.Services.Config;
using Pms.Domain.Services.Interface;
using Pms.ITSquarehub.Authentication.Service;
using Pms.Shared;

namespace Pms.Domain.Services
{
    public class AccountService : EntityService, IAccountService
    {
        private readonly IUserContext _userContext;
        private readonly IMicroServiceConfig _microServiceConfig;
        private readonly IITSAuthService _itsAuthService;
        private readonly IUserQuery userQuery;

        public AccountService(
            IMapper mapper,
            ILogger<AccountService> logger,
            IUserContext userContext,
            IMicroServiceConfig microServiceConfig,
            IITSAuthService itsAuthService,
            IUserQuery userQuery)
            : base(mapper, logger)
        {
            _userContext = userContext;
            _microServiceConfig = microServiceConfig;
            _itsAuthService = itsAuthService;
            this.userQuery = userQuery;
        }

        public async Task<Response<AuthUserIdentityDto>> GetIdentityAsync()
        {
            try
            {
                // Validate if the user is authenticated and has a valid token

                return Response<AuthUserIdentityDto>.Success(new AuthUserIdentityDto());
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error occurred while fetching identity.");
                return Response<AuthUserIdentityDto>.Exception(ex);
            }
        }

        public async Task<Response<AuthLoginDto>> LoginAsync(AuthLoginRequestDto loginRequest)
        {
            try
            {
                AuthLoginDto loginDto = new AuthLoginDto();

                if (loginRequest == null ||
                    loginRequest.Email == null ||
                    loginRequest.Password == null)
                {
                    return Response<AuthLoginDto>.Error(new ErrorDto(
                        Shared.Enums.ErrorCode.ValidationError, "Missing email and password."));
                }

                // Connect to ITS authentication
                var response = await _itsAuthService.LoginAsync(loginRequest.Email, loginRequest.Password);
                if (response == null || !response.Succeeded)
                {
                    // Connect to internal database
                    var result = await CheckLoginAsync(loginRequest.Email, loginRequest.Password);
                    if (!result.Item1)
                    {
                        return Response<AuthLoginDto>.Error(new ErrorDto(
                            Shared.Enums.ErrorCode.ValidationError, result.Item2));
                    }
                    loginDto.Succeeded = true;
                }
                else
                {
                    var identityResponse = await _itsAuthService.ConfirmIdentity(response.Data!.Token!);
                    if (identityResponse == null)
                    {
                        return Response<AuthLoginDto>.Error(new ErrorDto(
                            Shared.Enums.ErrorCode.ValidationError, "Could not obtain identity"));
                    }

                    loginDto.Succeeded = true;
                }

                // Generate the token

                return Response<AuthLoginDto>.Success(loginDto);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error occurred while logging in.");
                return Response<AuthLoginDto>.Exception(ex);
            }
        }

        public async Task<Response<AuthIdentityResultDto>> LogoutAsync()
        {
            // In case of using Identity Server, we can revoke the user logged in

            return Response<AuthIdentityResultDto>.Success(new AuthIdentityResultDto { Succeeded = true });
        }

        private object GenerateToken(User user)
        {
            // Generate JwtSecurityToken
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_microServiceConfig.JwtConfig!.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(1); // 1 day

            var generatedToken = new JwtSecurityToken(
                issuer: _microServiceConfig.JwtConfig.Issuer,
                audience: _microServiceConfig.JwtConfig.Audience,
                claims: new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(AuthClaims.FullName, user.FullName),
                    //new Claim(AuthClaims.Admin, user.IsAdmin.ToString()),

                    //new Claim(ClaimTypes.Role, user.Role.ToString())
                },
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(generatedToken);
        }

        private async Task<(bool, string)> CheckLoginAsync(string email, string password)
        {
            var isSuccessfull = false;
            try
            {
                var user = await userQuery.GetQuery(new()
                {
                    Email = email,
                    ShowPassword = true
                }).FirstOrDefaultAsync();
                if (user == null)
                {
                    return (isSuccessfull, "Email is not registered.");
                }

                if (user.Password != password)
                {
                    return (isSuccessfull, "Incorrect password.");
                }

                return (isSuccessfull, "OK");
            }
            catch (Exception ex)
            {
                return (isSuccessfull, "Exception occured.");
            }
        }
    }
}
