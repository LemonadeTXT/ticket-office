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
        private readonly IMapper _mapper;

        public ProfileController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        [Authorize]
        public IActionResult EditProfile()
        {
            var user = _userService.Get(int.Parse(User.Identity.Name));

            var userDto = _mapper.Map<User, UserProfileDto>(user);

            return View(userDto);
        }

        [HttpPost]
        [Authorize]
        public IActionResult EditProfile(UserProfileDto userDto)
        {
            if (ModelState.IsValid)
            {
                var user = _userService.Get(int.Parse(User.Identity.Name));

                if (!IsUsersEqual(userDto, user))
                {
                    _userService.Edit(userDto, user);

                    return RedirectToAction("Index", "Home");
                }
            }

            return View(userDto);
        }

        private bool IsUsersEqual(UserProfileDto userDto, User user)
        {
            if (userDto.Email != user.Email ||
                userDto.Login != user.Login ||
                userDto.Password != user.Password)
            {
                return false;
            }

            return true;
        }
    }
}
