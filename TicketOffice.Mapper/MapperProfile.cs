using AutoMapper;
using TicketOffice.Common.Dtos;
using TicketOffice.Common.Models;

namespace TicketOffice.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserCreateDto, User>().ReverseMap();
            CreateMap<UserProfileDto, User>().ReverseMap();
            CreateMap<UserDto, User>().ReverseMap();

            CreateMap<TicketDto, Ticket>().ReverseMap();
        }
    }
}
