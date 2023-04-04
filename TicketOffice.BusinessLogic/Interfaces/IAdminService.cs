﻿using TicketOffice.Common.Dtos;
using TicketOffice.Common.Models;
using Microsoft.AspNetCore.Http;

namespace TicketOffice.BusinessLogic.Interfaces
{
    public interface IAdminService
    {
        User GetUser(int id);

        List<UserDto> GetAllUsersDto();

        void CreateUser(UserDto userDto);

        void EditUser(UserDto userDto, User user);

        void DeleteUser(UserDto userDto);

        Ticket GetTicket(int id);

        List<TicketDto> GetAllTicketsDto();

        void CreateTicket(TicketDto ticketDto);

        void EditTicket(TicketDto ticketDto, Ticket ticket);

        void DeleteTicket(int id);

        bool EqualTickets(TicketDto ticketDto, Ticket ticket);

        byte[] ConvertAvatarToByteArray(HttpRequest files);
    }
}
