using authServer.Services;
using ClassLibrary.Dtos.Auth;
using ClassLibrary.Models;

namespace authServer;

// ReSharper disable once UnusedType.Global
public class AutoMapperProfile : ClassLibrary.Dtos.AutoMapperProfile
{
    public AutoMapperProfile()
    {
        HashService hashService = new();
        CreateMap<UserDto, User>()
            .BeforeMap(
                (src, dest) =>
                {
                    hashService.Hash(src.Password!, out var passwordHash, out var passwordSalt);
                    dest.Password = passwordHash;
                    dest.Salt = passwordSalt;
                }
            )
            .ForMember(dest => dest.Password, opt => opt.Ignore())
            .ForMember(dest => dest.Salt, opt => opt.Ignore())
            .ForMember(dest => dest.Licences, opt => opt.Ignore());
    }
}
