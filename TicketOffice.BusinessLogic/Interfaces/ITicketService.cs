using TicketOffice.Common.Dtos;
using TicketOffice.Common.Models;

namespace TicketOffice.BusinessLogic.Interfaces
{
    public interface ITicketService
    {
        Ticket Get(int ticketId);

        List<TicketDto> GetAllTicketsDto();

        List<TicketDto> FindTickets(TicketsFindDto ticketsFindDto);

        List<TicketDto> GetAllPurchasedTickets(int userId);

        void PurchaseTicket(int userId, int ticketId);

        bool IsPurchasedTicket(int userId, int ticketId);
    }
}