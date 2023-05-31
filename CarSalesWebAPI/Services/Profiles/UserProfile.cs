using AutoMapper;
using CarSalesWebAPI.Domain.Dtos.UserDtos;
using CarSalesWebAPI.Domain.Entities;

namespace CarSalesWebAPI.Services.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserTokenDto>().ReverseMap();
            CreateMap<User, UpdateUserDto>().ReverseMap()
                    .ForMember(dest => dest.IsDeleted, opt => opt.Ignore())
                    .ForMember(dest => dest.IsAdmin, opt => opt.Ignore());
            CreateMap<CreateUserAdmDto, User>().ForMember(dest => dest.IsAdmin,
                opt => opt.MapFrom(src => true)).ForMember(dest => dest.IsDeleted,
                opt => opt.MapFrom(src => false));
            CreateMap<CreateCommonUserDto, User>().ForMember(dest => dest.IsAdmin,
                opt => opt.MapFrom(src => false)).ForMember(dest => dest.IsDeleted,
                opt => opt.MapFrom(src => false));
            
        }
    }
}
