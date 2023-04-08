using TicketOffice.Common.Dtos;
using TicketOffice.Common.Models;

namespace TicketOffice.BusinessLogic.Interfaces
{
    public interface ITicketService
    {
        Ticket GetTicket(int ticketId);

        List<TicketDto> GetAllTicketsDto();

        void CreateTicket(TicketDto ticketDto);

        void EditTicket(TicketDto ticketDto, Ticket ticket);

        void DeleteTicket(int ticketId);

        bool EqualTickets(TicketDto ticketDto, Ticket ticket);

        List<TicketDto> FindTickets(TicketsFindDto ticketsFindDto);

        List<TicketDto> GetAllPurchasedTickets(int userId);

        void PurchaseTicket(int userId, int ticketId);

        bool IsPurchasedTicket(int userId, int ticketId);
    }
}