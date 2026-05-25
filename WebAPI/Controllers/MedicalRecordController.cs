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
            _logger.LogDebug("Started retrieving the list of all medical records");
            var records = _medicalRecordService.GetAll();
            _logger.LogDebug("Completed retrieving the list of all medical records");
            return Ok(records);
        }

        // GET api/medicalrecord/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<MedicalRecordDto> GetById(int id)
        {
            _logger.LogDebug("Started retrieving medical record with id {Id}", id);
            try
            {
                var record = _medicalRecordService.GetById(id);
                if (record == null)
                {
                    _logger.LogError($"Medical record with id {id} not found");
                    return NotFound("Medical record not found");
                }

                _logger.LogDebug("Completed retrieving medical record with id {Id}", id);
                return Ok(record);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving medical record with id {Id}", id);
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
            _logger.LogDebug("Started retrieving medical record by visit id {VisitId}", visitId);
            try
            {
                var record = _medicalRecordService.GetByVisitId(visitId);
                if (record == null)
                {
                    _logger.LogWarning("Medical record for visit id {VisitId} not found", visitId);
                    return NotFound("Medical record not found for this visit");
                }

                _logger.LogDebug("Completed retrieving medical record by visit id {VisitId}", visitId);
                return Ok(record);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving medical record by visit id {VisitId}", visitId);
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
            _logger.LogDebug("Started updating medical record with id {Id}", dto.Id);
            try
            {
                _medicalRecordService.Update(dto);
                _logger.LogDebug("Completed updating medical record with id {Id}", dto.Id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating medical record with id {Id}", dto.Id);
                return BadRequest(ex.Message);
            }
        }

    }
}

