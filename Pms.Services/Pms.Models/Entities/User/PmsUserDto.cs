using Pms.Shared.Models;

namespace Pms.Models
{
    public class PmsUserDto : EntityBaseDto
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
