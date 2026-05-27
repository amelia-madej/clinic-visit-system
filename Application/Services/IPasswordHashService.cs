namespace Application.Services;

public interface IPasswordHashService
{
    string Hash(string password);
    bool Verify(string password, string storedPassword);
    bool IsHash(string storedPassword);
}
