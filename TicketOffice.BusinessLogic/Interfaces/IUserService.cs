using TicketOffice.Common.Dtos;
using TicketOffice.Common.Models;

namespace TicketOffice.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        User Get(UserDto userDto);

        List<User> GetAll();

        void Create(UserDto userDto);

        void Edit(UserDto userDto);

        void Delete(UserDto userDto);
    }
}
