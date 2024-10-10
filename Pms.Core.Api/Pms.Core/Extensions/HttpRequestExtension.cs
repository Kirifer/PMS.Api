using Microsoft.AspNetCore.Http;

namespace Pms.Core.Extensions
{
    public static class HttpRequestExtension
    {
        /// <summary>
        /// Gets the full absolute URL from HTTP Request
        /// </summary>
        /// <param name="httpRequest">HttpRequest on where to obtain the full url</param>
        /// <returns>Absolute URL</returns>
        public static string GetAbsoluteUrl(this HttpRequest httpRequest)
        {
            return $"{httpRequest.Scheme}://{httpRequest.Host.Value}";
        }
    }
}
