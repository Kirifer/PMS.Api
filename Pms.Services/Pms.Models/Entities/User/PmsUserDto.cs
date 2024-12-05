using Pms.Shared.Models;

namespace Pms.Models
{
    public class PmsUserDto : EntityFullBaseDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Position { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool IsSupervisor { get; set; }
        public bool IsActive { get; set; }
        public bool? IsDeleted { get; set; }
        public Guid? ItsReferenceId { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}
