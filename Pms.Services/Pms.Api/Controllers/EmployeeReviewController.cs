//using System.Net;

//using Microsoft.AspNetCore.Mvc;

//using Pms.Core.Abstraction.Api;
//using Pms.Core.Filtering;
//using Pms.Domain.Services.Interface;
//using Pms.Models;

//namespace Pms.Api.Controllers
//{
//    [ApiController]
//    [ProducesResponseType(typeof(Response<>), (int)HttpStatusCode.Unauthorized)]
//    [ProducesResponseType(typeof(Response<>), (int)HttpStatusCode.Forbidden)]
//    [ProducesResponseType(typeof(Response<>), (int)HttpStatusCode.InternalServerError)]
//    public class EmployeeReviewController(IPerformanceReviewService performanceReviewService) : ApiControllerBase
//    {
//        /// <summary>
//        /// Creates the employee review
//        /// </summary>
//        /// <param name="payload">Request payload to create.</param>
//        /// <returns>The entity id of newly created recod.</returns>
//        [HttpPost]
//        [Route("employee-reviews")]
//        //[AuthorizePermission(AuthPermissions.PmsPerformanceReviewView)]
//        [ProducesResponseType(typeof(Response<IdDto>), (int)HttpStatusCode.OK)]
//        public async Task<IActionResult> CreatePerformanceReviewAsync([FromBody] PmsPerformanceReviewCreateDto payload)
//            => ApiResponse(await performanceReviewService.CreatePerformanceReviewAsync(payload));

//        /// <summary>
//        /// Updates the performance review
//        /// </summary>
//        /// <param name="id">Entity id to be updated.</param>
//        /// <param name="payload">Request payload to update.</param>
//        /// <returns>The entity id of updated recod.</returns>
//        [HttpPut]
//        [Route("employee-reviews/{id}")]
//        //[AuthorizePermission(AuthPermissions.PmsPerformanceReviewView)]
//        [ProducesResponseType(typeof(Response<IdDto>), (int)HttpStatusCode.OK)]
//        public async Task<IActionResult> UpdatePerformanceReviewAsync(Guid id, [FromBody] PmsPerformanceReviewUpdateDto payload)
//            => ApiResponse(await performanceReviewService.UpdatePerformanceReviewAsync(id, payload));

//        /// <summary>
//        /// Delets the performance review (soft-delete)
//        /// </summary>
//        /// <returns>return true or false</returns>
//        [HttpDelete]
//        [Route("performance-reviews/{id}")]
//        //[AuthorizePermission(AuthPermissions.PmsPerformanceReviewView)]
//        [ProducesResponseType(typeof(Response<bool>), (int)HttpStatusCode.OK)]
//        public async Task<IActionResult> DeletePerformanceReviewAsync(Guid id)
//            => ApiResponse(await performanceReviewService.DeletePerformanceReviewAsync(id));
//    }
//}
