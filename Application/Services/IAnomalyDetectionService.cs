using SharedKernel.DTOs;

namespace Application.Services
{
    public interface IAnomalyDetectionService
    {
        AnomalyDashboardDto DetectAnomalies(DateTime periodStart, DateTime periodEnd);
    }
}
