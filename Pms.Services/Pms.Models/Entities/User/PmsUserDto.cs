using Pms.Shared.Models;

namespace Pms.Models
{
    public class PmsUserDto : EntityBaseDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Position { get; set; }
        public bool? IsSupervisor { get; set; }
        public bool? IsActive { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}
