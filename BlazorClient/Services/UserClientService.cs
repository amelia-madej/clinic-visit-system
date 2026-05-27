using SharedKernel.DTOs;
using System.Net.Http.Json;

namespace BlazorClient.Services
{
    public interface IUserClientService
    {
        Task<UserDto?> GetByIdAsync(int userId);
        Task<bool> UpdateProfileAsync(UpdateUserProfileDto dto);
        Task<bool> UpdatePhotoAsync(UpdateUserPhotoDto dto);
        Task<bool> DeletePhotoAsync(int userId);
        Task<(bool Success, string? Error)> ChangePasswordAsync(ChangePasswordDto dto);
    }

    public class UserClientService : IUserClientService
    {
        private readonly HttpClient _http;

        public UserClientService(HttpClient http)
        {
            _http = http;
        }

        public Task<UserDto?> GetByIdAsync(int userId)
        {
            return _http.GetFromJsonAsync<UserDto>($"api/user/{userId}");
        }

        public async Task<bool> UpdateProfileAsync(UpdateUserProfileDto dto)
        {
            var response = await _http.PutAsJsonAsync($"api/user/{dto.UserId}/profile", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdatePhotoAsync(UpdateUserPhotoDto dto)
        {
            var response = await _http.PutAsJsonAsync($"api/user/{dto.UserId}/photo", dto);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeletePhotoAsync(int userId)
        {
            var response = await _http.DeleteAsync($"api/user/{userId}/photo");
            return response.IsSuccessStatusCode;
        }

        public async Task<(bool Success, string? Error)> ChangePasswordAsync(ChangePasswordDto dto)
        {
            var response = await _http.PutAsJsonAsync($"api/user/{dto.UserId}/password", dto);
            if (response.IsSuccessStatusCode)
                return (true, null);

            var error = await response.Content.ReadAsStringAsync();
            return (false, string.IsNullOrWhiteSpace(error) ? "Could not change password." : error.Trim('"'));
        }
    }
}
