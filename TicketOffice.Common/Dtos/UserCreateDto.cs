using System.ComponentModel.DataAnnotations;

namespace TicketOffice.Common.Dtos
{
    public class UserCreateDto
    {
        [Required(ErrorMessage = "Please, enter email!")]
        [StringLength(30,
            ErrorMessage = "The EMAIL is to big, please choice another!")]
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Please, enter login!")]
        [StringLength(20, MinimumLength = 3,
            ErrorMessage = "The LOGIN length must be between 3 and 20 characters!")]
        public string? Login { get; set; }

        [Required(ErrorMessage = "Please, enter password!")]
        [StringLength(20, MinimumLength = 5,
            ErrorMessage = "The PASSWORD length must be between 5 and 20 characters!")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Compare("Password", ErrorMessage = "Invalid password!")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }
    }
}
