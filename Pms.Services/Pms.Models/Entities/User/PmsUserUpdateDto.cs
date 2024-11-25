namespace Pms.Models
{
    public class PmsUserUpdateDto
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public bool IsSupervisor { get; set; }
        public bool? IsActive { get; set; }
    }
}