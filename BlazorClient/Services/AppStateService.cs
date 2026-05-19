using SharedKernel.DTOs;

namespace BlazorClient.Services
{
    public class AppStateService
    {
        public AuthResponseDto? CurrentUser { get; private set; }
        public PatientListItemDto? CurrentPatient { get; private set; }

        public event Action? OnChange;

        public void SetUser(AuthResponseDto? user)
        {
            CurrentUser = user;

            if (user?.PatientId is int patientId)
            {
                CurrentPatient = new PatientListItemDto
                {
                    PatientId = patientId,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                };
            }

            OnChange?.Invoke();
        }

        public void SetPatient(PatientListItemDto? patient)
        {
            CurrentPatient = patient;
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

            if (CurrentPatient is not null)
            {
                CurrentPatient.FirstName = user.FirstName;
                CurrentPatient.LastName = user.LastName;
            }

            OnChange?.Invoke();
        }

        public void Logout()
        {
            CurrentUser = null;
            CurrentPatient = null;
            OnChange?.Invoke();
        }
    }
}
