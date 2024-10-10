using System.Net.Http.Headers;

using Microsoft.Extensions.Logging;

using Pms.ITSquarehub.Authentication.Config;
using Pms.ITSquarehub.Authentication.Models;
using Pms.Shared.Constants;

namespace Pms.ITSquarehub.Authentication.Service
{
    public class AuthenticationService(
        HttpClient httpClient,
        IAuthenticationConfig config,
        ILogger<AuthenticationService> logger) : IITSAuthService
    {
        private readonly HttpClient httpClient = httpClient;
        private readonly ILogger<AuthenticationService> logger = logger;
        private readonly IAuthenticationConfig config = config;

        private HttpClient ApiHttpClient(string? authToken = null)
        {
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (!string.IsNullOrEmpty(authToken)) 
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(GlobalConstant.HttpSchemeBearer, authToken);

            return httpClient;
        }

        public async Task<ITSAuthLoginDto?> LoginAsync(string username, string password)
        {
            try
            {
                return await Task.FromResult(new ITSAuthLoginDto());
            }
            catch { }
            {
                return null;
            }
        }

        public async Task<ITSAuthIdentityDto?> ConfirmIdentity(string authToken)
        {
            try
            {
                return await Task.FromResult(new ITSAuthIdentityDto());
            }
            catch { }
            {
                return null;
            }
        }
    }
}
