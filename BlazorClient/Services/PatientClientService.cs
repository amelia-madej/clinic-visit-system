using SharedKernel.DTOs;
using System.Net.Http.Json;

namespace BlazorClient.Services
{
    public interface IPatientClientService
    {
        Task<(bool Success, string? Error)> RegisterAsync(PatientCreateDto dto);
    }

    public class PatientClientService : IPatientClientService
    {
        private readonly HttpClient _http;

        public PatientClientService(HttpClient http)
        {
            _http = http;
        }

        public async Task<(bool Success, string? Error)> RegisterAsync(PatientCreateDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/patient", dto);
            if (response.IsSuccessStatusCode)
                return (true, null);

            var error = await response.Content.ReadAsStringAsync();
            return (false, string.IsNullOrWhiteSpace(error) ? "Could not create patient account." : error.Trim('"'));
        }
    }
}
