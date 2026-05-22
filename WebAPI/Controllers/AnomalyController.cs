using Application.Services;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.DTOs;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AnomalyController : Controller
    {
        private readonly IAnomalyDetectionService _anomalyDetectionService;
        private readonly ILogger<AnomalyController> _logger;

        public AnomalyController(IAnomalyDetectionService anomalyDetectionService, ILogger<AnomalyController> logger)
        {
            _anomalyDetectionService = anomalyDetectionService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<AnomalyDashboardDto> GetAnomalies([FromQuery] DateTime? from, [FromQuery] DateTime? to)
        {
            try
            {
                var periodEnd = (to ?? DateTime.Today).Date;
                var periodStart = (from ?? periodEnd.AddDays(-30)).Date;

                _logger.LogDebug("Rozpoczęto wykrywanie anomalii.");
                var result = _anomalyDetectionService.DetectAnomalies(periodStart, periodEnd);
                _logger.LogDebug("Zakończono wykrywanie anomalii.");

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Błąd podczas wykrywania anomalii.");
                return BadRequest(ex.Message);
            }
        }
    }
}
