using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketOffice.BusinessLogic.Interfaces;
using TicketOffice.Common.Dtos;
using TicketOffice.Common.Models;

namespace TicketOffice.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IUserService _userService;
        private readonly ITicketService _ticketService;
        private readonly IMapper _mapper;

        public ProfileController(IUserService userService, ITicketService ticketService, IMapper mapper)
        {
            _userService = userService;
            _ticketService = ticketService;
            _mapper = mapper;
        }

        [Authorize]
        public IActionResult EditProfile()
        {
            var user = _userService.GetUser(int.Parse(User.Identity.Name));

            var userDto = _mapper.Map<User, UserProfileDto>(user);

            return View(userDto);
        }

        [HttpPost]
        [Authorize]
        public IActionResult EditProfile(UserProfileDto userProfileDto)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.GetUser(int.Parse(User.Identity.Name));

                _userService.EditUser(userProfileDto, user);

                return RedirectToAction("Index", "Home");
            }

            return View(userProfileDto);
        }

        [Authorize]
        public IActionResult YourTickets()
        {
            var ticketsDto = _ticketService.GetAllPurchasedTickets(int.Parse(User.Identity.Name));

            return View(ticketsDto);
        }
    }
}
