using Microsoft.AspNetCore.Http;

using Pms.Shared.Constants;

namespace Pms.Core.Abstraction
{
    public class HttpOnlyMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _jwtCookieName;

        public HttpOnlyMiddleware(RequestDelegate next, string jwtCookieName)
        {
            _next = next;
            _jwtCookieName = jwtCookieName;
        }

        public async Task Invoke(HttpContext context)
        {
            UpdateAuthenticationContext(context);

            await _next.Invoke(context);
        }

        /// <summary>
        /// Updates the authentication context on the Request Header
        /// </summary>
        /// <param name="context">HTTP Context to be updated</param>
        private void UpdateAuthenticationContext(HttpContext context)
        {
            var jwtCookie = context.Request.Cookies[_jwtCookieName];
            bool jwtHeaderIsNotPresent = !context.Request.Headers.ContainsKey(GlobalConstant.HttpHeaderAuthorization);
            bool jwtCookieIsExisting = !string.IsNullOrEmpty(jwtCookie);

            if (jwtHeaderIsNotPresent && jwtCookieIsExisting)
            {
                // This would assign the token from cookie to request header automatically
                context.Request.Headers.Append(GlobalConstant.HttpHeaderAuthorization, $"Bearer {jwtCookie}");
            }
        }
    }
}
