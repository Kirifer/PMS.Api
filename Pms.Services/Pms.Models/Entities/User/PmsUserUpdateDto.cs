namespace Pms.Models
{
    public class PmsUserUpdateDto
    {
        public required string Password { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
    }
}