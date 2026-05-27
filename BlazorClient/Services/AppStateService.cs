using Blazored.LocalStorage;
using SharedKernel.DTOs;
using System.Net.Http.Headers;

namespace BlazorClient.Services
{
    public class AppStateService
    {
        private const string UserStorageKey = "clinic.auth.user";
        private readonly HttpClient _http;
        private readonly ILocalStorageService _localStorage;

        public AppStateService(HttpClient http, ILocalStorageService localStorage)
        {
            _http = http;
            _localStorage = localStorage;
        }

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
            else
            {
                CurrentPatient = null;
            }

            OnChange?.Invoke();
        }

        public async Task InitializeAsync()
        {
            await _localStorage.RemoveItemAsync(UserStorageKey);

            if (CurrentUser is null)
                Logout();
        }

        public async Task SetUserAsync(AuthResponseDto? user)
        {
            SetUser(user);

            if (user is null)
            {
                await _localStorage.RemoveItemAsync(UserStorageKey);
                _http.DefaultRequestHeaders.Authorization = null;
                return;
            }

            if (!string.IsNullOrWhiteSpace(user.Token))
                ApplyAuthorizationHeader(user.Token);

            await _localStorage.RemoveItemAsync(UserStorageKey);
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
            _http.DefaultRequestHeaders.Authorization = null;
            OnChange?.Invoke();
        }

        public async Task LogoutAsync()
        {
            Logout();
            await _localStorage.RemoveItemAsync(UserStorageKey);
        }

        private void ApplyAuthorizationHeader(string token)
        {
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }
    }
}
