using System.Net;

using Pms.Core.Filtering;
using Pms.Shared;
using Pms.Shared.Enums;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using Pms.Core.Authentication;
using Pms.Core.Filtering;
using Pms.Shared.Enums;
using Pms.Shared;

namespace Pms.Core.Authentication.Attributes
{
    public class AuthorizePermissionAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly string[] _permissions;

        public AuthorizePermissionAttribute(params string[] permissions)
        {
            _permissions = permissions;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (!context.HttpContext.User.Identity!.IsAuthenticated) { return; }

            // Validate by user id information.
            var userContextService = context.HttpContext.RequestServices
                .GetService(typeof(IUserContext)) as IUserContext;
            if (userContextService?.UserId == Guid.Empty)
            {
                context.Result = MapApiErrorResponse();
                return;
            }
        }

        private ObjectResult MapApiErrorResponse()
        {
            var error = new ErrorDto(ErrorCode.RequestForbidden, "You are forbidden to access this resource.");
            var response = Response<ErrorDto>.Error(HttpStatusCode.Forbidden, error);

            return new ObjectResult(response) { StatusCode = (int)HttpStatusCode.Forbidden };
        }
    }
}
