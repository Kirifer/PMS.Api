using Microsoft.AspNetCore.Mvc;

using Pms.Core.Filtering;

namespace Pms.Core.Abstraction.Api
{
    public abstract class ApiControllerBase : ControllerBase
    {
        /// <summary>
        /// Converts the API response to JSON Entity Equivalent
        /// </summary>
        /// <typeparam name="TEntity">Entity type to be mapped</typeparam>
        /// <param name="response">Response to be converted</param>
        protected ObjectResult ApiResponse<TEntity>(Response<TEntity> response)
        {
            return response.Succeeded ?
                MapApiSuccessResponse(response) :
                MapApiErrorResponse(response);
        }

        /// <summary>
        /// Converts the API response to JSON Collection Equivalent
        /// </summary>
        /// <typeparam name="TEntity">Entity type to be mapped</typeparam>
        /// <param name="response">Response to be converted</param>
        protected ObjectResult ApiResponse<TEntity>(Response<List<TEntity>> response)
        {
            return response.Succeeded ?
                MapApiSuccessResponse(response) :
                MapApiErrorResponse(response);
        }

        /// <summary>
        /// Map the API Entity Successful Response
        /// </summary>
        /// <typeparam name="TEntity">Entity type to be mapped</typeparam>
        /// <param name="response">Response to be converted</param>
        private ObjectResult MapApiSuccessResponse<TEntity>(Response<TEntity> response)
        {
            return StatusCode((int)response.Code, response);
        }

        /// <summary>
        /// Map the API Collection Entity Successful Response
        /// </summary>
        /// <typeparam name="TEntity">Entity type to be mapped</typeparam>
        /// <param name="response">Response to be converted</param>
        private ObjectResult MapApiSuccessResponse<TEntity>(Response<List<TEntity>> response)
        {
            return StatusCode((int)response.Code, response);
        }

        /// <summary>
        /// Map the API Error Response
        /// </summary>
        /// <param name="response">Response to be converted</param>
        private ObjectResult MapApiErrorResponse<TEntity>(Response<TEntity> response)
        {
            return StatusCode((int)response.Code, response);
        }

        /// <summary>
        /// Map the API List Error Response
        /// </summary>
        /// <param name="response">Response to be converted</param>
        private ObjectResult MapApiErrorResponse<TEntity>(Response<List<TEntity>> response)
        {
            return StatusCode((int)response.Code, response);
        }
    }
}
