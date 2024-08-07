using Pms.Core.Database.Abstraction;
using Pms.Datalayer.Interface;

namespace Pms.Datalayer.Entities
{
    public class User : DbEntityIdBase
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
        public required string Email { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
    }
}
