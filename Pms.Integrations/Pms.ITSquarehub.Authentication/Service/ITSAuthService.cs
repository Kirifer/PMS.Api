using System.Net.Http.Headers;
using System.Text;

using Microsoft.Extensions.Logging;

using Pms.Core.Extensions;
using Pms.Core.Filtering;
using Pms.ITSquarehub.Authentication.Config;
using Pms.ITSquarehub.Authentication.Models;
using Pms.Shared.Constants;

namespace Pms.ITSquarehub.Authentication.Service
{
    public class ITSAuthService(
        HttpClient httpClient,
        IAuthenticationConfig config,
        ILogger<ITSAuthService> logger) : IITSAuthService
    {
        private readonly HttpClient httpClient = httpClient;
        private readonly ILogger<ITSAuthService> logger = logger;
        private readonly IAuthenticationConfig config = config;

        private HttpClient ApiHttpClient(string? authToken = null)
        {
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (!string.IsNullOrEmpty(authToken)) 
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(GlobalConstant.HttpSchemeBearer, authToken);

            return httpClient;
        }

        public async Task<Response<ITSAuthLoginDto>?> LoginAsync(string username, string password)
        {
            try
            {
                var apiUrl = $"{config.BaseUrl}/login";
                var content = new StringContent(new ITSAuthLoginRequest() {
                    Username = username,
                    Password = password
                }.SerializeEntity(), Encoding.UTF8, "application/json");

                var apiResponse = await httpClient.PostAsync(apiUrl, content);
                string responseContent = await apiResponse.Content.ReadAsStringAsync();
                if (!apiResponse.IsSuccessStatusCode)
                {

                }
                return responseContent.DeserializeEntity<Response<ITSAuthLoginDto>>();
            }
            catch { }
            {
                return null;
            }
        }

        public async Task<Response<ITSAuthIdentityDto>?> ConfirmIdentity(string authToken)
        {
            try
            {
                var apiUrl = $"{config.BaseUrl}/identity";
                var apiResponse = await ApiHttpClient(authToken).GetAsync(apiUrl);
                string responseContent = await apiResponse.Content.ReadAsStringAsync();
                if (!apiResponse.IsSuccessStatusCode)
                {

                }

                return responseContent.DeserializeEntity<Response<ITSAuthIdentityDto>>();
            }
            catch { }
            {
                return null;
            }
        }
    }
}
