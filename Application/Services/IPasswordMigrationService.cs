namespace Application.Services;

public interface IPasswordMigrationService
{
    int HashPlainTextPasswords();
}
