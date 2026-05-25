using SharedKernel.DTOs;

namespace BlazorServer.Services;

public class AppStateService
{
    private readonly ILogger<AppStateService> _logger;

    public AppStateService(ILogger<AppStateService> logger)
    {
        _logger = logger;
    }

    public AuthResponseDto? CurrentUser { get; private set; }
    public DoctorListItemDto? CurrentDoctor { get; private set; }

    public event Action? OnChange;

    public void SetUser(AuthResponseDto? user)
    {
        _logger.LogInformation("Setting current user. UserId: {UserId}", user?.UserId);
        CurrentUser = user;
        CurrentDoctor = null;

        if (user?.DoctorId is int doctorId)
        {
            CurrentDoctor = new DoctorListItemDto
            {
                DoctorId = doctorId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Specialization = string.Empty
            };
        }

        OnChange?.Invoke();
    }

    public void SetDoctor(DoctorListItemDto? doctor)
    {
        _logger.LogDebug("Setting current doctor. DoctorId: {DoctorId}", doctor?.DoctorId);
        CurrentDoctor = doctor;
        OnChange?.Invoke();
    }

    public void RefreshCurrentUser(UserDto user)
    {
        if (CurrentUser is null || CurrentUser.UserId != user.UserId)
        {
            _logger.LogDebug("Skipping current user refresh. State user is null or IDs do not match.");
            return;
        }

        _logger.LogInformation("Refreshing current user data for UserId: {UserId}", user.UserId);
        CurrentUser.FirstName = user.FirstName;
        CurrentUser.LastName = user.LastName;
        CurrentUser.FullName = $"{user.FirstName} {user.LastName}";
        CurrentUser.Email = user.Email;
        CurrentUser.PhoneNumber = user.PhoneNumber;
        CurrentUser.PhotoDataUrl = user.PhotoDataUrl;

        if (CurrentDoctor is not null)
        {
            CurrentDoctor.FirstName = user.FirstName;
            CurrentDoctor.LastName = user.LastName;
        }

        OnChange?.Invoke();
    }

    public void Logout()
    {
        _logger.LogInformation("Clearing current user and doctor from app state.");
        CurrentUser = null;
        CurrentDoctor = null;
        OnChange?.Invoke();
    }
}
