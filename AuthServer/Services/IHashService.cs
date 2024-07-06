namespace authServer.Services;

public interface IHashService
{
    void Hash(string password, out byte[] passwordHash, out byte[] passwordSalt);
    bool VerifyHash(string password, byte[] passwordHash, byte[] passwordSalt);
}
