using System.Net;

using Microsoft.AspNetCore.Mvc;

using Pms.Core.Abstraction.Api;
using Pms.Core.Filtering;
using Pms.Domain.Services.Interface;
using Pms.Models;

namespace Pms.Api.Controllers
{
    //[Authorize]
    [ApiController]
    [ProducesResponseType(typeof(Response<>), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(Response<>), (int)HttpStatusCode.Forbidden)]
    [ProducesResponseType(typeof(Response<>), (int)HttpStatusCode.InternalServerError)]
    public class LookupController(ILookupService lookupService) : ApiControllerBase
    {

        /// <summary>
        /// Gets the competencies
        /// </summary>
        /// <returns>The list of users.</returns>
        [HttpGet]
        [Route("lookup/competencies")]
        //[AuthorizePermission(AuthPermissions.PmsPerformanceReviewView)]
        [ProducesResponseType(typeof(Response<List<PmsCompetencyDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetCompetenciesAsync([FromQuery]PmsCompetencyFilterDto filter)
            => ApiResponse(await lookupService.GetCompetenciesAsync(filter));

        /// <summary>
        /// Gets the users
        /// </summary>
        /// <returns>The list of users.</returns>
        [HttpGet]
        [Route("lookup/users")]
        //[AuthorizePermission(AuthPermissions.PmsPerformanceReviewView)]
        [ProducesResponseType(typeof(Response<List<PmsCompetencyDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetUsersAsync()
            => ApiResponse(await lookupService.GetUsersAsync());

        /// <summary>
        /// Gets the supervivors
        /// </summary>
        /// <returns>The list of users.</returns>
        [HttpGet]
        [Route("lookup/supervisors")]
        //[AuthorizePermission(AuthPermissions.PmsPerformanceReviewView)]
        [ProducesResponseType(typeof(Response<List<PmsCompetencyDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetSupervisorsAsync()
            => ApiResponse(await lookupService.GetSupervisorsAsync());
    }
}
