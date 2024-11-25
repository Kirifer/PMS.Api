
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using AutoMapper;

using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

using Pms.Core.Abstraction;
using Pms.Core.Authentication;
using Pms.Core.Filtering;
using Pms.Datalayer.Entities;
using Pms.Datalayer.Interface;
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

        public AccountService(
            IMapper mapper,
            ILogger<AccountService> logger,
            IUserContext userContext,
            IMicroServiceConfig microServiceConfig,
            IITSAuthService itsAuthService)
            : base(mapper, logger)
        {
            _userContext = userContext;
            _microServiceConfig = microServiceConfig;
            _itsAuthService = itsAuthService;
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
                if (loginRequest == null ||
                    loginRequest.UserName == null ||
                    loginRequest.Password == null)
                {
                    return Response<AuthLoginDto>.Error(new ErrorDto(
                        Shared.Enums.ErrorCode.ValidationError, "Missing username and password."));
                }

                var response = await _itsAuthService.LoginAsync(loginRequest.UserName, loginRequest.Password);
                if (response == null || !response.Succeeded)
                {
                    return Response<AuthLoginDto>.Error(new ErrorDto(
                        Shared.Enums.ErrorCode.ValidationError, "Incorrect login."));
                }

                var identityResponse = await _itsAuthService.ConfirmIdentity(response.Data!.Token!);
                if (identityResponse == null)
                {
                    return Response<AuthLoginDto>.Error(new ErrorDto(
                        Shared.Enums.ErrorCode.ValidationError, "Could not obtain identity"));
                }

                return Response<AuthLoginDto>.Success(new AuthLoginDto());
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
    }
}
