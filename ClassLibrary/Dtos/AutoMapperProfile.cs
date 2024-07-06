using AutoMapper;
using ClassLibrary.Dtos.Auth;
using ClassLibrary.Dtos.SharedDtos;
using ClassLibrary.Enums;
using ClassLibrary.Models;

namespace ClassLibrary.Dtos;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<User, UserDto>()
            .ForMember(dest => dest.Password, opt =>
                opt.MapFrom<User>(src => null!));
        CreateMap<User, UserShortDto>();
        CreateMap<User, IdDto>();
        CreateMap<User, UserUpdateDto>();
        CreateMap<Gender, IdDto>();
        CreateMap<Gender, IdCodeDto>();
        CreateMap<Device, DeviceDto>()
            .ForMember(dest => dest.Active, opt => opt.MapFrom(src =>
                src.Token != null &&
                DateTime.Now.Subtract(src.Token.DateTime).TotalMinutes < Globals.TokenExpiryMinutes));
        CreateMap<Notification, NotificationDto>();
        CreateMap<NotificationDto, Notification>();
        CreateMap<Licence, LicenceDto>().ReverseMap();
    }
}
