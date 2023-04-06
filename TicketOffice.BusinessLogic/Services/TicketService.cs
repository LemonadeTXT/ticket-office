using AutoMapper;
using TicketOffice.BusinessLogic.Interfaces;
using TicketOffice.Common.Dtos;
using TicketOffice.Common.Models;
using TicketOffice.DAL;
using TicketOffice.Models;

namespace TicketOffice.BusinessLogic.Services
{
    public class TicketService : ITicketService
    {
        private readonly ApplicationContext _applicationContext;
        private readonly IMapper _mapper;

        public TicketService(ApplicationContext applicationContext, IMapper mapper)
        {
            _applicationContext = applicationContext;
            _mapper = mapper;
        }

        public Ticket Get(int ticketId)
        {
            var ticket = _applicationContext.Tickets.FirstOrDefault(t => t.Id == ticketId);

            return ticket;
        }

        public List<TicketDto> GetAllTicketsDto()
        {
            var tickets = _applicationContext.Tickets.ToList();

            var ticketsDto = new List<TicketDto>();

            foreach (var ticket in tickets)
            {
                ticketsDto.Add(_mapper.Map<Ticket, TicketDto>(ticket));
            }

            return ticketsDto;
        }

        public List<TicketDto> FindTickets(TicketsFindDto ticketsFindDto)
        {
            var tickets = _applicationContext.Tickets.ToList();

            if (ticketsFindDto.EventDate >= DateTime.Today)
            {
                tickets.RemoveAll(t => t.EventDate.Date != ticketsFindDto.EventDate);
            }

            if (ticketsFindDto.EventName != null)
            {
                tickets.RemoveAll(t => !(t.EventName.Contains(ticketsFindDto.EventName)));
            }

            if (ticketsFindDto.NumberOfSeats != null)
            {
                tickets.RemoveAll(t => t.NumberOfSeats < ticketsFindDto.NumberOfSeats);
            }

            var ticketsDto = new List<TicketDto>();

            foreach (var ticket in tickets)
            {
                ticketsDto.Add(_mapper.Map<Ticket, TicketDto>(ticket));
            }

            return ticketsDto;
        }

        public List<TicketDto> GetAllPurchasedTickets(int userId)
        {
            var purchasedtickets = _applicationContext.PurchasedTickets.Where(p => p.UserId == userId).ToList();

            var tickets = new List<Ticket>();

            foreach (var purchasedticket in purchasedtickets)
            {
                tickets.Add(_applicationContext.Tickets.FirstOrDefault(t => t.Id == purchasedticket.TicketId));
            }

            var ticketsDto = new List<TicketDto>();

            foreach (var ticket in tickets)
            {
                ticketsDto.Add(_mapper.Map<Ticket, TicketDto>(ticket));
            }

            return ticketsDto;
        }

        public void PurchaseTicket(int userId, int ticketId)
        {
            var purchasedTicket = new PurchasedTicket { UserId = userId, TicketId = ticketId };

            _applicationContext.PurchasedTickets.Add(purchasedTicket);

            var ticket = Get(ticketId);

            var editedTicket = ticket;
            editedTicket.NumberOfSeats--;

            _applicationContext.Entry(ticket).CurrentValues.SetValues(editedTicket);
            _applicationContext.SaveChanges();
        }

        public bool IsPurchasedTicket(int userId, int ticketId)
        {
            var purchasedTicket = _applicationContext.PurchasedTickets.FirstOrDefault(p => 
            p.UserId == userId && p.TicketId == ticketId);

            if (purchasedTicket == null)
            {
                return false;
            }

            return true;
        }
    }
}
