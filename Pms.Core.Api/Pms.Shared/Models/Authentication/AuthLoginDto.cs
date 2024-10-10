namespace Pms.Shared
{
    public class AuthLoginDto
    {
        public bool Succeeded { get; set; }

        public bool IsLockedOut { get; set; }

        public bool IsNotAllowed { get; set; }

        public bool RequiresTwoFactor { get; set; }

        public string? Token { get; set; }
    }
}

