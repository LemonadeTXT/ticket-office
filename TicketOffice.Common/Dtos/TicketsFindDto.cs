using System.ComponentModel.DataAnnotations;

namespace TicketOffice.Common.Dtos
{
    public class TicketsFindDto
    {
        [DataType(DataType.Date)]
        public DateTime EventDate { get; set; }

        [StringLength(30,
            ErrorMessage = "The EVENT NAME is to big, please change it!")]
        public string? EventName { get; set; }

        [Range(1, 100, ErrorMessage = "The value must be beetwen 1 and 100!")]
        public int? NumberOfSeats { get; set; }
    }
}
