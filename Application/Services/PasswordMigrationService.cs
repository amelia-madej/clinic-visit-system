using Domain.Contracts;

namespace Application.Services;

public class PasswordMigrationService : IPasswordMigrationService
{
    private readonly IClinicUnitOfWork _uow;
    private readonly IPasswordHashService _passwordHashService;

    public PasswordMigrationService(IClinicUnitOfWork uow, IPasswordHashService passwordHashService)
    {
        _uow = uow;
        _passwordHashService = passwordHashService;
    }

    public int HashPlainTextPasswords()
    {
        var users = _uow.UserRepository.GetAll();
        var updatedCount = 0;

        foreach (var user in users)
        {
            if (_passwordHashService.IsHash(user.Password))
                continue;

            user.Password = _passwordHashService.Hash(user.Password);
            updatedCount++;
        }

        if (updatedCount > 0)
            _uow.Commit();

        return updatedCount;
    }
}
