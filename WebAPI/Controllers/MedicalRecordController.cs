using Application.Services;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.DTOs;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicalRecordController : ControllerBase
    {
        private readonly IMedicalRecordService _medicalRecordService;
        private readonly ILogger<MedicalRecordController> _logger;

        public MedicalRecordController(
            IMedicalRecordService medicalRecordService,
            ILogger<MedicalRecordController> logger)
        {
            _medicalRecordService = medicalRecordService;
            _logger = logger;
        }

        // GET api/medicalrecord
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<MedicalRecordDto>> GetAll()
        {
            _logger.LogDebug("Rozpoczęto pobieranie listy wszystkich rekordów medycznych");
            var records = _medicalRecordService.GetAll();
            _logger.LogDebug("Zakończono pobieranie listy wszystkich rekordów medycznych");
            return Ok(records);
        }

        // GET api/medicalrecord/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<MedicalRecordDto> GetById(int id)
        {
            _logger.LogDebug($"Rozpoczęto pobieranie rekordu medycznego o id {id}");
            try
            {
                var record = _medicalRecordService.GetById(id);
                if (record == null)
                {
                    _logger.LogError($"Medical record with id {id} not found");
                    return NotFound("Medical record not found");
                }

                _logger.LogDebug($"Zakończono pobieranie rekordu medycznego o id {id}");
                return Ok(record);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/medicalrecord/visit/{visitId}
        [HttpGet("visit/{visitId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<MedicalRecordDto> GetByVisitId(int visitId)
        {
            try
            {
                var record = _medicalRecordService.GetByVisitId(visitId);
                if (record == null)
                    return NotFound("Medical record not found for this visit");

                return Ok(record);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/medicalrecord
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Update([FromBody] MedicalRecordDto dto)
        {
            _logger.LogDebug($"Rozpoczęto aktualizację rekordu medycznego o id {dto.MedicalRecordId}");
            try
            {
                _medicalRecordService.Update(dto);
                _logger.LogDebug($"Zakończono aktualizację rekordu medycznego o id {dto.MedicalRecordId}");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Błąd podczas aktualizacji rekordu medycznego: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

    }
}
