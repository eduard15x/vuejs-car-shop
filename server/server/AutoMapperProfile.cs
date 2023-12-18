using AutoMapper;
using server.Dtos.Car;
using server.Dtos.User;
using server.Models;

namespace server
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Car, GetSingleCarDto>().ReverseMap();
            CreateMap<Car, AddNewCarDto>().ReverseMap();
            CreateMap<Car, UpdateCarDto>().ReverseMap();

            CreateMap<User, LoginUserDto>().ReverseMap();
            CreateMap<User, RegisterUserDto>().ReverseMap();
        }
    }
}
