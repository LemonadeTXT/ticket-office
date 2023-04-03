using TicketOffice.Common.Dtos;
using TicketOffice.Common.Models;

namespace TicketOffice.BusinessLogic.Interfaces
{
    public interface IAdminService
    {
        User GetUserDto(int id);

        List<UserDto> GetAllUsersDto();

        void CreateUser(UserDto userDto);

        void EditUser(UserDto userDto, User user);

        void DeleteUser(UserDto userDto);
    }
}
