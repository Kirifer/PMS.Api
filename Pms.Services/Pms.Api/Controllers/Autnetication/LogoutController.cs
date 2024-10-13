using System.Net;

using Pms.Core.Config;
using Pms.Core.Extensions;
using Pms.Core.Filtering;
using Pms.Domain.Services.Interface;
using Pms.Shared;
using Pms.Shared.Extensions;

using Microsoft.AspNetCore.Mvc;

using Pms.Core.Config;
using Pms.Core.Filtering;

namespace Pms.Api.Controllers
{
    [ApiController]
    public class LogoutController(
        IMicroServiceConfig config,
        IAccountService accountService) : ControllerBase
    {
        private readonly IMicroServiceConfig config = config;
        private readonly IAccountService accountService = accountService;

        /// <summary>
        /// Logout the logged in user
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("logout")]
        [ProducesResponseType(typeof(Response<AuthIdentityResultDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> LogoutAsync()
        {
            var response = await accountService.LogoutAsync();
            var uriHost = new Uri(Request.GetAbsoluteUrl());
            var cookieOptions = new CookieOptions
            {
                Domain = uriHost.GetDomainName()
            };

            Response.Cookies.Delete(config.JwtConfig!.CookieName, cookieOptions);
            return StatusCode((int)response.Code, response);
        }
    }
}
