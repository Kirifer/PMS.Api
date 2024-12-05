using Pms.Core.Database.Abstraction;

namespace Pms.Datalayer.Entities
{
    public class User : DbEntityIdBase
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool IsSupervisor { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public Guid? ItsReferenceId { get; set; }

        public string FullName => $"{FirstName} {LastName}";
    }
}
