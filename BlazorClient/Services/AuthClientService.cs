using SharedKernel;
using SharedKernel.DTOs;
using System.Net.Http.Json;

namespace BlazorClient.Services
{
    public interface IAuthClientService
    {
        Task<AuthResponseDto?> LoginAsync(LoginDto dto);
    }

    public class AuthClientService : IAuthClientService
    {
        private readonly HttpClient _http;

        public AuthClientService(HttpClient http)
        {
            _http = http;
        }

        public async Task<AuthResponseDto?> LoginAsync(LoginDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/auth/login", dto);

            if (!response.IsSuccessStatusCode)
                return null;

            var auth = await response.Content.ReadFromJsonAsync<AuthResponseDto>();
            return auth?.Role == UserRole.Patient ? auth : null;
        }
    }
}
