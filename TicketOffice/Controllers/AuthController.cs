using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TicketOffice.Common.Dtos;
using TicketOffice.Common.Models;
using Microsoft.AspNetCore.Authorization;
using TicketOffice.BusinessLogic.Interfaces;

namespace TicketOffice.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public AuthController(IAuthService authService, IUserService userService)
        {
            _authService = authService;
            _userService = userService;
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserAuthDto userAuthDto)
        {
            if (ModelState.IsValid)
            {
                if (_authService.IsLogin(userAuthDto.Login, userAuthDto.Password, out int id))
                {
                    var user = _userService.GetUser(id);

                    await Authenticate(user);

                    return RedirectToAction("Index", "Home");
                }
            }

            return View(userAuthDto);
        }

        public IActionResult SignUp()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(UserCreateDto userCreateDto)
        {
            if (ModelState.IsValid)
            {
                if (!_authService.IsRegistration(userCreateDto.Login, userCreateDto.Email))
                {
                    _userService.CreateUserByUserCreateDto(userCreateDto);

                    return RedirectToAction(nameof(Login));
                }
            }

            return View(userCreateDto);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }

        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

            var claimIdentity =
                new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimIdentity));
        }
    }
}
