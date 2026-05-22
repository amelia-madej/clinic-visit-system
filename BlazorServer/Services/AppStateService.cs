using SharedKernel.DTOs;

namespace BlazorServer.Services;

public class AppStateService
{
    public AuthResponseDto? CurrentUser { get; private set; }
    public DoctorListItemDto? CurrentDoctor { get; private set; }

    public event Action? OnChange;

    public void SetUser(AuthResponseDto? user)
    {
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
        CurrentDoctor = doctor;
        OnChange?.Invoke();
    }

    public void RefreshCurrentUser(UserDto user)
    {
        if (CurrentUser is null || CurrentUser.UserId != user.UserId)
            return;

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
        CurrentUser = null;
        CurrentDoctor = null;
        OnChange?.Invoke();
    }
}
