using authServer.Exceptions;
using authServer.Repositories.UserRepository;
using AutoMapper;
using ClassLibrary.Dtos.Auth;
using ClassLibrary.Models;

namespace authServer.Services.AuthService;

public interface IChangePasswordService
{
    Task<UserDto> ChangePassword(Int64 userId, NewPasswordDto newPassword);
}

public class ChangePasswordService(
    IUserRepository userRepository,
    IMapper mapper,
    IHashService hashService
) : IChangePasswordService
{
    public async Task<UserDto> ChangePassword(Int64 userId, NewPasswordDto newPassword)
    {
        User dbUser = (await userRepository.GetAsync(u=> u.Id.Equals(userId)))!;
        if (!hashService.VerifyHash(newPassword.OldPassword, dbUser.Password, dbUser.Salt))
        {
            throw new IncorrectFormDataException("incorrectOldPassword");
        }

        if (!newPassword.ReNewPassword.Equals(newPassword.NewPassword))
        {
            throw new IncorrectFormDataException("youMustProvideValidPasswordConfirmation");
        }
        hashService.Hash(
            newPassword.ReNewPassword,
            out byte[] passwordHash,
            out byte[] passwordSalt
        );
        dbUser.Password = passwordHash;
        dbUser.Salt = passwordSalt;
        await userRepository.SaveAsync();
        return mapper.Map<UserDto>(dbUser);
    }
}
