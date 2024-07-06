using authServer.Repositories.LicenceRepository;
using authServer.Repositories.UserRepository;
using AutoMapper;
using ClassLibrary.Dtos.Auth;
using ClassLibrary.Enums;
using ClassLibrary.Models;

namespace authServer.Services.AuthService;

public interface IRegisterSuperAdminService
{
    Task<UserDto> RegisterSuperAdmin(UserDto user);
}

public class RegisterSuperAdminService(
    IGettableLicenceRepository gettableLicenceRepository,
    ISettableUserRepository settableUserRepository,
    IMapper mapper,
    IHashService hashService
) : IRegisterSuperAdminService
{
    public async Task<UserDto> RegisterSuperAdmin(UserDto user)
    {
        hashService.Hash(user.Password!, out byte[] passwordHash, out byte[] passwordSalt);
        var newUser = new User
        {
            Email = user.Email,
            Password = passwordHash,
            Salt = passwordSalt,
            HrtLicence = true,
            Verified = true,
            LombanditLicence = true,
            Licences = new() { (await gettableLicenceRepository.GetByIdAsync(1))! },
            Role = Role.SUPERADMIN
        };
        settableUserRepository.Create(newUser);
        await settableUserRepository.SaveAsync();
        return mapper.Map<UserDto>(user);
    }
}
