using SharedKernel.DTOs;

namespace BlazorClient.Services
{
    public class AppStateService
    {
        public PatientListItemDto? CurrentPatient { get; private set; }

        public event Action? OnChange;

        public void SetPatient(PatientListItemDto? patient)
        {
            CurrentPatient = patient;
            OnChange?.Invoke();
        }
    }
}
