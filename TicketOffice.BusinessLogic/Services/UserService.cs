using AutoMapper;
using TicketOffice.BusinessLogic.Interfaces;
using TicketOffice.Common.Dtos;
using TicketOffice.Common.Models;
using TicketOffice.DAL;

namespace TicketOffice.BusinessLogic.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationContext _applicationContext;
        private readonly IMapper _mapper;

        public UserService(ApplicationContext applicationContext, IMapper mapper)
        {
            _applicationContext = applicationContext;
            _mapper = mapper;
        }

        public User GetUser(int userId)
        {
            var user = _applicationContext.Users.FirstOrDefault(u => u.Id == userId);

            return user;
        }

        public List<User> GetAllUsers()
        {
            var users = _applicationContext.Users.ToList();

            return users;
        }

        public List<UserDto> GetAllUsersDto()
        {
            var users = _applicationContext.Users.ToList();

            var usersDto = new List<UserDto>();

            foreach (var user in users)
            {
                usersDto.Add(_mapper.Map<User, UserDto>(user));
            }

            return usersDto;
        }

        public void CreateUserByUserCreateDto(UserCreateDto userCreateDto)
        {
            var user = _mapper.Map<UserCreateDto, User>(userCreateDto);

            _applicationContext.Users.Add(user);
            _applicationContext.SaveChanges();
        }

        public void CreateUserByUserDto(UserDto userDto)
        {
            var user = _mapper.Map<UserDto, User>(userDto);

            _applicationContext.Users.Add(user);
            _applicationContext.SaveChanges();
        }

        public void EditUserByUserProfileDto(UserProfileDto userProfileDto, User user)
        {
            _applicationContext.Entry(user).CurrentValues.SetValues(userProfileDto);
            _applicationContext.SaveChanges();
        }

        public void EditUserByUserDto(UserDto userDto, User user)
        {
            _applicationContext.Entry(user).CurrentValues.SetValues(userDto);
            _applicationContext.SaveChanges();
        }

        public void DeleteUser(UserDto userDto)
        {
            var user = _mapper.Map<UserDto, User>(userDto);

            _applicationContext.Users.Remove(user);
            _applicationContext.SaveChanges();
        }
    }
}
