using SharedKernel.DTOs;

namespace BlazorServer.Services;

public class AppStateService
{
    public DoctorListItemDto? CurrentDoctor { get; private set; }

    public event Action? OnChange;

    public void SetDoctor(DoctorListItemDto? doctor)
    {
        CurrentDoctor = doctor;
        OnChange?.Invoke();
    }
}
