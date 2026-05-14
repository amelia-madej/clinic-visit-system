using SharedKernel.DTOs;
using System.Net.Http.Json;

namespace BlazorClient.Services
{
    public interface IVisitService
    {
        Task<List<VisitListItemDto>> GetByPatientIdAsync(int patientId);
        Task<VisitDetailsDto?> GetByIdAsync(int visitId);
        Task<bool> CreateAsync(VisitCreateDto dto);
        Task<PrescriptionDetailsDto?> GetPrescriptionByIdAsync(int visitId);
    }
    public class VisitService : IVisitService
    {
        private readonly HttpClient _http;
        public VisitService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<VisitListItemDto>> GetByPatientIdAsync(int patientId)
        {
            return await _http.GetFromJsonAsync<List<VisitListItemDto>>($"api/visit/patient/{patientId}")
                   ?? new List<VisitListItemDto>();
        }

        public async Task<VisitDetailsDto?> GetByIdAsync(int visitId)
        {
            return await _http.GetFromJsonAsync<VisitDetailsDto>($"api/visit/{visitId}");
        }

        public async Task<PrescriptionDetailsDto?> GetPrescriptionByIdAsync(int receiptId)
        {
            return await _http.GetFromJsonAsync<PrescriptionDetailsDto>($"api/prescription/{receiptId}");
        }

        public async Task<bool> CreateAsync(VisitCreateDto dto)
        {
            var response = await _http.PostAsJsonAsync("api/visit", dto);
            return response.IsSuccessStatusCode;
        }
    }
}
