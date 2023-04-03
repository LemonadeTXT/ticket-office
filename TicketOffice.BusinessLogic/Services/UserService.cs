﻿using AutoMapper;
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

        public User Get(int id)
        {
            var user = _applicationContext.Users.FirstOrDefault(u => u.Id == id);

            return user;
        }

        public List<User> GetAll()
        {
            var users = _applicationContext.Users.ToList();

            return users;
        }

        public void Create(UserCreateDto userCreateDto)
        {
            var user = _mapper.Map<UserCreateDto, User>(userCreateDto);

            _applicationContext.Users.Add(user);
            _applicationContext.SaveChanges();
        }

        public void Edit(UserProfileDto userDto, User user)
        {
            _applicationContext.Entry(user).CurrentValues.SetValues(userDto);
            _applicationContext.SaveChanges(true);
        }
    }
}
