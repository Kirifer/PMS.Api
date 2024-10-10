namespace Pns.Core.Config.Authentication
{
    public class JwtConfig
    {
        public string CookieName { get; set; } = string.Empty;
        public int ExpiryDays { get; set; } = 1;
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string Key { get; set; } = string.Empty;
    }
}
