using System.Security.Cryptography;

namespace Application.Services;

public class PasswordHashService : IPasswordHashService
{
    private const string Prefix = "PBKDF2";
    private const string Version = "V1";
    private const int SaltSize = 16;
    private const int HashSize = 32;
    private const int Iterations = 100_000;

    public string Hash(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Password cannot be empty.", nameof(password));

        var salt = RandomNumberGenerator.GetBytes(SaltSize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(
            password,
            salt,
            Iterations,
            HashAlgorithmName.SHA256,
            HashSize);

        return string.Join(
            "$",
            Prefix,
            Version,
            Iterations,
            Convert.ToBase64String(salt),
            Convert.ToBase64String(hash));
    }

    public bool Verify(string password, string storedPassword)
    {
        if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(storedPassword))
            return false;

        if (!IsHash(storedPassword))
            return storedPassword == password;

        var parts = storedPassword.Split('$');
        if (parts.Length != 5 || parts[0] != Prefix || parts[1] != Version)
            return false;

        if (!int.TryParse(parts[2], out var iterations))
            return false;

        try
        {
            var salt = Convert.FromBase64String(parts[3]);
            var expectedHash = Convert.FromBase64String(parts[4]);
            var actualHash = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                iterations,
                HashAlgorithmName.SHA256,
                expectedHash.Length);

            return CryptographicOperations.FixedTimeEquals(actualHash, expectedHash);
        }
        catch (FormatException)
        {
            return false;
        }
    }

    public bool IsHash(string storedPassword)
    {
        return storedPassword.StartsWith($"{Prefix}$", StringComparison.Ordinal);
    }
}
