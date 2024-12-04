using System.Net;

using Microsoft.AspNetCore.Mvc;

using Pms.Core.Extensions;
using Pms.Core.Filtering;
using Pms.Domain.Services.Config;
using Pms.Domain.Services.Interface;
using Pms.Shared;
using Pms.Shared.Extensions;

namespace Pms.Api.Controllers
{
    public class LoginController(
        IMicroServiceConfig serviceConfig,
        IAccountService accountService) : ControllerBase
    {
        private readonly IAccountService accountService = accountService;

        /// <summary>
        /// Login the user based on credentials
        /// </summary>
        /// <param name="request">The request details of the user to be logged in</param>
        [HttpPost]
        [Route("login")]
        [ProducesResponseType(typeof(Response<AuthLoginDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> LoginAsync([FromBody] AuthLoginRequestDto request)
        {
            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);
            }

            var uriHost = new Uri(Request.GetAbsoluteUrl());
            var response = await accountService.LoginAsync(request);

            if (response.Succeeded && !Equals(response.Data?.Token, null))
            {
                var cookieOptions = new CookieOptions
                {
                    Expires = DateTime.Now.AddDays(1),
                    HttpOnly = true,
                    Domain = uriHost.GetDomainName(),
                    Secure = true,
                    SameSite = SameSiteMode.None // Disable for localhost
                };

                Response.Cookies.Append(serviceConfig.JwtConfig!.CookieName, response.Data?.Token!, cookieOptions);
            }

            return StatusCode((int)response.Code, response);
        }
    }
}
