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
            return View(_adminService.GetAllUsersDto());
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
            var user = _adminService.GetUserDto(id);

            var userDto = _mapper.Map<User, UserDto>(user);

            return View(userDto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult EditUser(UserDto userDto)
        {
            if (ModelState.IsValid)
            {
                var user = _adminService.GetUserDto(userDto.Id);

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
            return View();
        }
    }
}
