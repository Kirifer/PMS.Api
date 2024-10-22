
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using AutoMapper;

using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

using Pms.Core.Abstraction;
using Pms.Core.Authentication;
using Pms.Core.Config;
using Pms.Core.Filtering;
using Pms.Datalayer.Entities;
using Pms.Datalayer.Interface;
using Pms.Domain.Services.Interface;
using Pms.Shared;

namespace Pms.Domain.Services
{
    public class AccountService : EntityService, IAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserContext _userContext;
        private readonly IMicroServiceConfig _microServiceConfig;

        public AccountService(
            IMapper mapper,
            ILogger<AccountService> logger,
            IUserContext userContext,
            IMicroServiceConfig microServiceConfig,

            IUserRepository userRepository)
            : base(mapper, logger)
        {
            _userRepository = userRepository;
            _userContext = userContext;
            _microServiceConfig = microServiceConfig;
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
