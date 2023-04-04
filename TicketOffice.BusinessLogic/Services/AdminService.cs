using AutoMapper;
using Microsoft.AspNetCore.Http;
using TicketOffice.BusinessLogic.Interfaces;
using TicketOffice.Common.Dtos;
using TicketOffice.Common.Models;
using TicketOffice.DAL;

namespace TicketOffice.BusinessLogic.Services
{
    public class AdminService : IAdminService
    {
        private readonly ApplicationContext _applicationContext;
        private readonly IMapper _mapper;

        public AdminService(ApplicationContext applicationContext, IMapper mapper)
        {
            _applicationContext = applicationContext;
            _mapper = mapper;
        }

        public User GetUser(int id)
        {
            var user = _applicationContext.Users.FirstOrDefault(u => u.Id == id);

            return user;
        }

        public List<UserDto> GetAllUsersDto()
        {
            var users = _applicationContext.Users.ToList();

            var usersDto = new List<UserDto>();

            foreach (var user in users)
            {
                usersDto.Add(_mapper.Map<User, UserDto>(user));
            }

            return usersDto;
        }

        public void CreateUser(UserDto userDto)
        {
            var user = _mapper.Map<UserDto, User>(userDto);

            _applicationContext.Users.Add(user);
            _applicationContext.SaveChanges();
        }

        public void EditUser(UserDto userDto, User user)
        {
            _applicationContext.Entry(user).CurrentValues.SetValues(userDto);
            _applicationContext.SaveChanges(true);
        }

        public void DeleteUser(UserDto userDto)
        {
            var user = _mapper.Map<UserDto, User>(userDto);

            _applicationContext.Users.Attach(user);
            _applicationContext.Users.Remove(user);
            _applicationContext.SaveChanges();
        }

        public Ticket GetTicket(int id)
        {
            var ticket = _applicationContext.Tickets.FirstOrDefault(u => u.Id == id);

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

        public void CreateTicket(TicketDto ticketDto)
        {
            var ticket = _mapper.Map<TicketDto, Ticket>(ticketDto);

            _applicationContext.Tickets.Add(ticket);
            _applicationContext.SaveChanges();
        }

        public void EditTicket(TicketDto ticketDto, Ticket ticket)
        {
            _applicationContext.Entry(ticket).CurrentValues.SetValues(ticketDto);
            _applicationContext.SaveChanges(true);
        }

        public void DeleteTicket(int id)
        {
            var ticket = GetTicket(id);

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

        public byte[] ConvertAvatarToByteArray(HttpRequest files)
        {
            byte[] avatarByteArray = Array.Empty<byte>();

            foreach (var file in files.Form.Files)
            {
                using (var memoryStream = new MemoryStream())
                {
                    file.CopyTo(memoryStream);

                    avatarByteArray = memoryStream.ToArray();
                }
            }

            return avatarByteArray;
        }
    }
}
