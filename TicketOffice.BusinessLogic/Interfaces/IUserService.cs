using TicketOffice.Common.Dtos;
using TicketOffice.Common.Models;

namespace TicketOffice.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        User GetUser(int userId);

        List<User> GetAllUsers();

        List<UserDto> GetAllUsersDto();

        void CreateUser(UserCreateDto userCreateDto);

        void CreateUser(UserDto userDto);

        void EditUser(UserProfileDto userProfileDto, User user);

        void EditUser(UserDto userDto, User user);

        void DeleteUser(UserDto userDto);
    }
}
