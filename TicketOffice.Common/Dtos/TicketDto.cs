using System.ComponentModel.DataAnnotations;

namespace TicketOffice.Common.Dtos
{
    public class TicketDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please, enter the DATE and TIME of event!")]
        [DataType(DataType.DateTime)]
        public DateTime EventDate { get; set; }

        [Required(ErrorMessage = "Please, enter EVENT NAME!")]
        [StringLength(30,
            ErrorMessage = "The EVENT NAME is to big, please change it!")]
        public string? EventName { get; set; }

        [Required(ErrorMessage = "Please, enter EVENT TYPE!")]
        [StringLength(30,
            ErrorMessage = "The EVENT TYPE is to big, please change it!")]
        public string? EventType { get; set; }

        [Required(ErrorMessage = "Please, enter NUMBER OF FREE SEATS!")]
        [Range(1, 100, ErrorMessage = "The value must be beetwen 1 and 100!")]
        public int? NumberOfSeats { get; set; }

        public byte[]? QR { get; set; }
    }
}
