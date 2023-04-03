using TicketOffice.Common.Dtos;
using TicketOffice.Common.Models;

namespace TicketOffice.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        User Get(int id);

        List<User> GetAll();

        void Create(UserCreateDto userCreateDto);

        void Edit(UserProfileDto userDto, User user);
    }
}
