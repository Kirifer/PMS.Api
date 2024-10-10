namespace Pms.Shared.Extensions
{
    public static class UriExtensions
    {
        /// <summary>
        /// Gets the domain name of the Host URI
        /// </summary>
        /// <param name="uri">URI on where to obtain the domain name</param>
        /// <returns>Domain Name</returns>
        public static string GetDomainName(this Uri uri)
        {
            if (Equals(uri, null)) return string.Empty;

            var actualHost = string.IsNullOrWhiteSpace(uri.Host) ?
                uri.AbsoluteUri : uri.Host;
            var subDomain = uri.GetSubDomainName();

            return string.IsNullOrWhiteSpace(subDomain) ?
                actualHost : actualHost.Replace(subDomain, string.Empty);
        }

        /// <summary>
        /// Gets the subdomain of the Host Uri
        /// </summary>
        /// <param name="uri">URI on where to obtain the sub-domain name</param>
        /// <returns>Subdomain Name</returns>
        public static string GetSubDomainName(this Uri uri)
        {
            if (Equals(uri, null)) return string.Empty;

            var actualHost = string.IsNullOrWhiteSpace(uri.Host) ?
                uri.AbsoluteUri : uri.Host;
            var splittedHost = actualHost.Split('.');

            // In case of no subdomain setup like localhost return string empty
            return (splittedHost.Length <= 2) ?
                string.Empty : splittedHost[0];
        }
    }
}