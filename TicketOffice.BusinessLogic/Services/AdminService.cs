using AutoMapper;
using TicketOffice.BusinessLogic.Interfaces;
using TicketOffice.Common.Dtos;
using TicketOffice.Common.Models;
using TicketOffice.DAL;

namespace TicketOffice.BusinessLogic.Services
{
    public class AdminService : IAdminService
    {
        private readonly ApplicationContext _applicationContext;
        private readonly IMapper _mapper;

        public AdminService(ApplicationContext applicationContext, IMapper mapper)
        {
            _applicationContext = applicationContext;
            _mapper = mapper;
        }

        public User GetUserDto(int id)
        {
            var user = _applicationContext.Users.FirstOrDefault(u => u.Id == id);

            return user;
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

        public void CreateUser(UserDto userDto)
        {
            var user = _mapper.Map<UserDto, User>(userDto);

            _applicationContext.Users.Add(user);
            _applicationContext.SaveChanges();
        }

        public void EditUser(UserDto userDto, User user)
        {
            _applicationContext.Entry(user).CurrentValues.SetValues(userDto);
            _applicationContext.SaveChanges(true);
        }

        public void DeleteUser(UserDto userDto)
        {
            var user = _mapper.Map<UserDto, User>(userDto);

            _applicationContext.Users.Attach(user);
            _applicationContext.Users.Remove(user);
            _applicationContext.SaveChanges();
        }
    }
}
