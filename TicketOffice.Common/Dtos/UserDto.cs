using System.ComponentModel.DataAnnotations;

namespace TicketOffice.Common.Dtos
{
    public class UserDto
    {
        public string? Login { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
    }
}
