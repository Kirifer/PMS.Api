using System.Net;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Pms.Core.Filtering;
using Pms.Domain.Services.Interface;

namespace Pms.Api.Controllers
{
    [Authorize()]
    [ApiController]
    [ProducesResponseType(typeof(Response<>), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(Response<>), (int)HttpStatusCode.Forbidden)]
    [ProducesResponseType(typeof(Response<>), (int)HttpStatusCode.InternalServerError)]
    public class IdentityController(IAccountService accountService) : ControllerBase
    {
        private readonly IAccountService accountService = accountService;

        /// <summary>
        /// Gets the identity of the currently logged in user.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("identity")]
        public async Task<IActionResult> GetIdentityAsync()
        {
            var response = await accountService.GetIdentityAsync();
            return StatusCode((int)response.Code, response);
        }
    }
}
