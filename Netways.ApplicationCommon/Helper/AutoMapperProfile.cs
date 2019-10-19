using AutoMapper;
using Netways.Application.Dtos;
using Netways.EntityFramworkCore.Model;

namespace Netways.ApplicationCommon.Helper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserDto, User>();
            CreateMap<Country, CountryDto>();
            CreateMap<CountryDto, Country>();
        }
    }
}
