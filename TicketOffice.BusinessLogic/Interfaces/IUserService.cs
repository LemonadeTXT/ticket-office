using TicketOffice.Common.Dtos;
using TicketOffice.Common.Models;

namespace TicketOffice.BusinessLogic.Interfaces
{
    public interface IUserService
    {
        User GetUser(int userId);

        List<User> GetAllUsers();

        List<UserDto> GetAllUsersDto();

        void CreateUserByUserCreateDto(UserCreateDto userCreateDto);

        void CreateUserByUserDto(UserDto userDto);

        void EditUserByUserProfileDto(UserProfileDto userProfileDto, User user);

        void EditUserByUserDto(UserDto userDto, User user);

        void DeleteUser(UserDto userDto);
    }
}
