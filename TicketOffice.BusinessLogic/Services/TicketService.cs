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

        public Ticket GetTicket(int ticketId)
        {
            var ticket = _applicationContext.Tickets.FirstOrDefault(t => t.Id == ticketId);

            return ticket;
        }

        public List<TicketDto> GetAllTicketsDto()
        {
            var tickets = _applicationContext.Tickets.AsQueryable();

            var ticketsDto = new List<TicketDto>();

            foreach (var ticket in tickets)
            {
                ticketsDto.Add(_mapper.Map<Ticket, TicketDto>(ticket));
            }

            return ticketsDto;
        }

        public void CreateTicket(TicketDto ticketDto)
        {
            var ticket = _mapper.Map<TicketDto, Ticket>(ticketDto);

            _applicationContext.Tickets.Add(ticket);
            _applicationContext.SaveChanges();
        }

        public void EditTicket(TicketDto ticketDto, Ticket ticket)
        {
            _applicationContext.Entry(ticket).CurrentValues.SetValues(ticketDto);
            _applicationContext.SaveChanges();
        }

        public void DeleteTicket(int ticketId)
        {
            var ticket = GetTicket(ticketId);

            _applicationContext.Tickets.Attach(ticket);
            _applicationContext.Tickets.Remove(ticket);
            _applicationContext.SaveChanges();
        }

        public bool EqualTickets(TicketDto ticketDto, Ticket ticket)
        {
            if (ticketDto.EventDate != ticket.EventDate ||
                ticketDto.EventName != ticket.EventName ||
                ticketDto.EventType != ticket.EventType ||
                ticketDto.NumberOfSeats != ticket.NumberOfSeats ||
                (ticketDto.QR != Array.Empty<byte>() && ticketDto.QR != ticket.QR))
            {
                return false;
            }

            return true;
        }

        public List<TicketDto> FindTickets(TicketsFindDto ticketsFindDto)
        {
            var tickets = _applicationContext.Tickets.AsQueryable();

            if (ticketsFindDto.EventDate >= DateTime.Today)
            {
                tickets = tickets.Where(t => t.EventDate.Date == ticketsFindDto.EventDate);
            }

            if (ticketsFindDto.EventName != null)
            {
                tickets = tickets.Where(t => t.EventName.Contains(ticketsFindDto.EventName));
            }

            if (ticketsFindDto.NumberOfSeats != null)
            {
                tickets = tickets.Where(t => t.NumberOfSeats >= ticketsFindDto.NumberOfSeats);
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
            var purchasedTicketsId = _applicationContext.PurchasedTickets
                .Where(p => p.UserId == userId)
                .Select(p => p.TicketId)
                .AsQueryable();

            var ticketsId = new List<int>();
            ticketsId.AddRange(purchasedTicketsId);

            var tickets = new List<Ticket>();
            tickets.AddRange(_applicationContext.Tickets.Where(t => ticketsId.Contains(t.Id)));

            var ticketsDto = new List<TicketDto>();

            foreach (var ticket in tickets)
            {
                ticketsDto.Add(_mapper.Map<Ticket, TicketDto>(ticket));
            }

            return ticketsDto;
        }

        public void PurchaseTicket(int userId, int ticketId)
        {
            var boughtTicket = new PurchasedTicket
            {
                UserId = userId,
                TicketId = ticketId
            };

            _applicationContext.PurchasedTickets.Add(boughtTicket);

            var ticket = GetTicket(ticketId);
            ticket.NumberOfSeats--;

            _applicationContext.Tickets.Update(ticket);
            _applicationContext.SaveChanges();
        }

        public bool IsPurchasedTicket(int userId, int ticketId)
        {
            var isBoughtTicket = _applicationContext.PurchasedTickets.Any(p =>
            p.UserId == userId && p.TicketId == ticketId);

            return isBoughtTicket;
        }
    }
}
