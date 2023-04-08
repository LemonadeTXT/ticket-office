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
        private readonly IUserService _userService;
        private readonly ITicketService _ticketService;
        private readonly IImageService _imageService;
        private readonly IMapper _mapper;

        public AdminController(IUserService userService, ITicketService ticketService, IImageService imageService, IMapper mapper)
        {
            _userService = userService;
            _ticketService = ticketService;
            _imageService = imageService;
            _mapper = mapper;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult UsersManagement()
        {
            var usersDto = _userService.GetAllUsersDto();

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
                _userService.CreateUserByUserDto(userDto);

                return RedirectToAction("UsersManagement");
            }

            return View(userDto);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult EditUser(int id)
        {
            var user = _userService.GetUser(id);

            var userDto = _mapper.Map<User, UserDto>(user);

            return View(userDto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult EditUser(UserDto userDto)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.GetUser(userDto.Id);

                if (user.Role != Common.Enum.Role.Admin)
                {
                    _userService.EditUserByUserDto(userDto, user);

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
                _userService.DeleteUser(userDto);
            }

            return RedirectToAction("UsersManagement");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult TicketsManagement()
        {
            var ticketsDto = _ticketService.GetAllTicketsDto();

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
                ticketDto.QR = _imageService.ConvertAvatarToByteArray(Request);

                if (ticketDto.QR != Array.Empty<byte>())
                {
                    _ticketService.CreateTicket(ticketDto);

                    return RedirectToAction("TicketsManagement");
                }
            }

            return View(ticketDto);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult EditTicket(int id)
        {
            var ticket = _ticketService.GetTicket(id);

            var ticketDto = _mapper.Map<Ticket, TicketDto>(ticket);

            return View(ticketDto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult EditTicket(TicketDto ticketDto)
        {
            if (ModelState.IsValid)
            {
                ticketDto.QR = _imageService.ConvertAvatarToByteArray(Request);

                var ticket = _ticketService.GetTicket(ticketDto.Id);

                if (!_ticketService.EqualTickets(ticketDto, ticket))
                {
                    if (ticketDto.QR == Array.Empty<byte>())
                    {
                        ticketDto.QR = ticket.QR;
                    }

                    _ticketService.EditTicket(ticketDto, ticket);

                    return RedirectToAction("TicketsManagement");
                }
            }

            return View(ticketDto);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult DeleteTicket(int id)
        {
            _ticketService.DeleteTicket(id);

            return RedirectToAction("TicketsManagement");
        }
    }
}
