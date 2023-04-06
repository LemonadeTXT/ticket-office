using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketOffice.BusinessLogic.Interfaces;
using TicketOffice.Common.Dtos;
using TicketOffice.Common.Models;

namespace TicketOffice.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IMapper _mapper;

        public AdminController(IAdminService adminService, IMapper mapper)
        {
            _adminService = adminService;
            _mapper = mapper;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult UsersManagement()
        {
            var usersDto = _adminService.GetAllUsersDto();

            return View(usersDto);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateUser(UserDto userDto)
        {
            if (ModelState.IsValid)
            {
                _adminService.CreateUser(userDto);

                return RedirectToAction("UsersManagement");
            }

            return View(userDto);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult EditUser(int id)
        {
            var user = _adminService.GetUser(id);

            var userDto = _mapper.Map<User, UserDto>(user);

            return View(userDto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult EditUser(UserDto userDto)
        {
            if (ModelState.IsValid)
            {
                var user = _adminService.GetUser(userDto.Id);

                if (user.Role != Common.Enum.Role.Admin)
                {
                    _adminService.EditUser(userDto, user);

                    return RedirectToAction("UsersManagement");
                }
            }

            return View(userDto);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult DeleteUser(UserDto userDto)
        {
            if (userDto.Role != Common.Enum.Role.Admin)
            {
                _adminService.DeleteUser(userDto);
            }

            return RedirectToAction("UsersManagement");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult TicketsManagement()
        {
            var ticketsDto = _adminService.GetAllTicketsDto();

            return View(ticketsDto);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult CreateTicket()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateTicket(TicketDto ticketDto)
        {
            if (ModelState.IsValid)
            {
                ticketDto.QR = _adminService.ConvertAvatarToByteArray(Request);

                if (ticketDto.QR != Array.Empty<byte>())
                {
                    _adminService.CreateTicket(ticketDto);

                    return RedirectToAction("TicketsManagement");
                }
            }

            return View(ticketDto);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult EditTicket(int id)
        {
            var ticket = _adminService.GetTicket(id);

            var ticketDto = _mapper.Map<Ticket, TicketDto>(ticket);

            return View(ticketDto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult EditTicket(TicketDto ticketDto)
        {
            if (ModelState.IsValid)
            {
                ticketDto.QR = _adminService.ConvertAvatarToByteArray(Request);

                var ticket = _adminService.GetTicket(ticketDto.Id);

                if (!_adminService.EqualTickets(ticketDto, ticket))
                {
                    if (ticketDto.QR == Array.Empty<byte>())
                    {
                        ticketDto.QR = ticket.QR;
                    }

                    _adminService.EditTicket(ticketDto, ticket);

                    return RedirectToAction("TicketsManagement");
                }
            }

            return View(ticketDto);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult DeleteTicket(int id)
        {
            _adminService.DeleteTicket(id);

            return RedirectToAction("TicketsManagement");
        }
    }
}
