using TicketOffice.BusinessLogic.Interfaces;
using TicketOffice.Common.Dtos;
using TicketOffice.Common.Models;
using TicketOffice.DAL;

namespace TicketOffice.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationContext _applicationContext;

        public UserService(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public void Create(UserDto userDto)
        {
            throw new NotImplementedException();
        }

        public void Delete(UserDto userDto)
        {
            throw new NotImplementedException();
        }

        public void Edit(UserDto userDto)
        {
            throw new NotImplementedException();
        }

        public User Get(UserDto userDto)
        {
            throw new NotImplementedException();
        }

        public List<User> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
