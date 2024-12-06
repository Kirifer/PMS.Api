using System.Net;
using Microsoft.AspNetCore.Mvc;
using Pms.Core.Abstraction.Api;
using Pms.Core.Filtering;
using Pms.Datalayer.Queries;
using Pms.Domain.Services;
using Pms.Domain.Services.Interface;
using Pms.Models;

namespace Pms.Api.Controllers
{
    [ApiController]
    [ProducesResponseType(typeof(Response<>), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(Response<>), (int)HttpStatusCode.Forbidden)]
    [ProducesResponseType(typeof(Response<>), (int)HttpStatusCode.InternalServerError)]

    public class UserPerformanceReviewController(IUserPerformanceReviewsService
        userPerformanceReviewsService) : ApiControllerBase
    {
        /// <summary>
        /// Gets the user performance reviews
        /// </summary>
        /// <returns>The list of user performance reviews.</returns>
 
        [HttpGet]
        [Route("users/{id}/performance-review")]
        [ProducesResponseType(typeof(Response<List<PmsUserPerformanceReviewDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetUserPerformanceReviewsAsync([FromQuery] PmsUserPerformanceReviewFilterDto filter)
            => ApiResponse(await userPerformanceReviewsService.GetUserPerformanceReviewsAsync(filter));

        /// <summary>
        /// Gets the user performance review detail
        /// </summary>
        /// <returns>User performance review.</returns>
        //[HttpGet]
        //[Route("user-performance-reviews/{id}")]
        //[ProducesResponseType(typeof(Response<PmsUserPerformanceReviewDto>), (int)HttpStatusCode.OK)]
        //public async Task<IActionResult> GetUserPerformanceReviewAsync(Guid id)
        //    => ApiResponse(await _userPerformanceReviewService.GetUserPerformanceReviewAsync(id));

        /// <summary>
        /// Creates the user performance review
        /// </summary>
        /// <param name = "payload" > Request payload to create.</param>
        /// <returns>The entity id of newly created record.</returns>
        [HttpPost]
        [Route("users/{id}/performance-reviews")]
        [ProducesResponseType(typeof(Response<IdDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateUserPerformanceReviewAsync([FromBody] PmsUserPerformanceReviewCreateDto payload)
            => ApiResponse(await userPerformanceReviewService.CreateUserPerformanceReviewAsync(payload));

        /// <summary>
        /// Updates the user performance review
        /// </summary>
        /// <param name="id">Entity id to be updated.</param>
        /// <param name="payload">Request payload to update.</param>
        /// <returns>The entity id of updated record.</returns>
        //[HttpPut]
        //[Route("user-performance-reviews/{id}")]
        //[ProducesResponseType(typeof(Response<IdDto>), (int)HttpStatusCode.OK)]
        //public async Task<IActionResult> UpdateUserPerformanceReviewAsync(Guid id, [FromBody] PmsUserPerformanceReviewUpdateDto payload)
        //    => ApiResponse(await _userPerformanceReviewService.UpdateUserPerformanceReviewAsync(id, payload));

        /// <summary>
        /// Deletes the user performance review (soft-delete)
        /// </summary>
        /// <returns>Return true or false</returns>
        //[HttpDelete]
        //[Route("user-performance-reviews/{id}")]
        //[ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
        //public async Task<IActionResult> DeleteUserPerformanceReviewAsync(Guid id)
        //    => ApiResponse(await _userPerformanceReviewService.DeleteUserPerformanceReviewAsync(id));
    }
}
