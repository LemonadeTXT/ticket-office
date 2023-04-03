using System.ComponentModel.DataAnnotations;

namespace TicketOffice.Common.Dtos
{
    public class UserAuthDto
    {
        [Required(ErrorMessage = "Please, enter login!")]
        [StringLength(15, MinimumLength = 3,
            ErrorMessage = "The LOGIN length must be between 3 and 15 characters!")]
        public string? Login { get; set; }

        [Required(ErrorMessage = "Please, enter password!")]
        [StringLength(15, MinimumLength = 5,
            ErrorMessage = "The PASSWORD length must be between 5 and 15 characters!")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
