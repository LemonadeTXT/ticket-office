using TicketOffice.BusinessLogic.Interfaces;
using TicketOffice.Common.Models;

namespace TicketOffice.BusinessLogic.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserService _userService;

        public AuthService(IUserService userService)
        {
            _userService = userService;
        }

        public bool IsLogin(string login, string password, out int userId)
        {
            userId = -1;

            foreach (User user in _userService.GetAll())
            {
                if (user.Login == login &&
                    user.Password == password)
                {
                    userId = user.Id;

                    return true;
                }
            }

            return false;
        }

        public bool IsRegistration(string login, string email)
        {
            foreach (User user in _userService.GetAll())
            {
                if (user.Login == login || user.Email == email)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
