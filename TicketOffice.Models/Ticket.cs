namespace TicketOffice.Common.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public DateTime EventDate { get; set; }
        public string? EventName { get; set; }
        public string? EventType { get; set; }
        public int? NumberOfSeats { get; set; }
        public byte[]? QR { get; set;}
    }
}
