using System.Net;

using Microsoft.AspNetCore.Mvc;

using Pms.Core.Filtering;
using Pms.Domain.Services.Interface;
using Pms.Models;

namespace Pms.Api.Controllers
{
    [ApiController]
    [ProducesResponseType(typeof(Response<>), (int)HttpStatusCode.Unauthorized)]
    [ProducesResponseType(typeof(Response<>), (int)HttpStatusCode.Forbidden)]
    [ProducesResponseType(typeof(Response<>), (int)HttpStatusCode.InternalServerError)]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("users")]
        [ProducesResponseType(typeof(Response<List<PmsUserDto>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetUserAsync([FromQuery] PmsUserFilterDto filter)
        {
            var response = await _userService.GetUsersAsync(filter);
            return StatusCode((int)response.Code, response);
        }

        [HttpGet]
        [Route("users/{id}")]
        [ProducesResponseType(typeof(Response<PmsUserDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetUserAsync(Guid id)
        {
            var response = await _userService.GetUserAsync(id);
            return StatusCode((int)response.Code, response);
        }

        [HttpPost]
        [Route("users")]
        [ProducesResponseType(typeof(Response<IdDto>), (int)HttpStatusCode.Created)]
        public async Task<IActionResult> AddUserAsync([FromBody] PmsUserCreateDto user)
        {
            var response = await _userService.CreateUserAsync(user);
            return StatusCode((int)response.Code, response);
        }

        [HttpPut]
        [Route("users/{id}")]
        [ProducesResponseType(typeof(Response<IdDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateUserAsync(Guid id, [FromBody] PmsUserUpdateDto user)
        {
            var response = await _userService.UpdateUserAsync(id, user);
            return StatusCode((int)response.Code, response);
        }

        [HttpDelete]
        [Route("users/{id}")]
        [ProducesResponseType(typeof(Response<IdDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteUserAsync(Guid id)
        {
            var response = await _userService.DeleteUserAsync(id);
            return StatusCode((int)response.Code, response);
        }
    }
}
