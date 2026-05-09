using Application.Services;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.DTOs;
using System;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SickLeaveController : ControllerBase
    {
        private readonly ISickLeaveService _sickLeaveService;
        private readonly ILogger<SickLeaveController> _logger;

        public SickLeaveController(
            ISickLeaveService sickLeaveService,
            ILogger<SickLeaveController> logger)
        {
            _sickLeaveService = sickLeaveService;
            _logger = logger;
        }

        // GET api/sickleave
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<SickLeaveListItemDto>> GetAll()
        {
            _logger.LogDebug("Rozpoczęto pobieranie listy wszystkich zwolnień lekarskich");
            var sickLeaves = _sickLeaveService.GetAll();
            _logger.LogDebug("Zakończono pobieranie listy wszystkich zwolnień lekarskich");
            return Ok(sickLeaves);
        }

        // GET api/sickleave/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<SickLeaveDetailsDto> GetById(int id)
        {
            _logger.LogDebug($"Rozpoczęto pobieranie zwolnienia lekarskiego o id {id}");
            try
            {
                var sickLeave = _sickLeaveService.GetById(id);
                if (sickLeave == null)
                {
                    _logger.LogError($"Sick leave with id {id} not found");
                    return NotFound("Sick leave not found");
                }

                _logger.LogDebug($"Zakończono pobieranie zwolnienia lekarskiego o id {id}");
                return Ok(sickLeave);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/sickleave/medicalrecord/{medicalRecordId}
        [HttpGet("medicalrecord/{medicalRecordId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<SickLeaveDetailsDto> GetByMedicalRecordId(int medicalRecordId)
        {
            try
            {
                var sickLeave = _sickLeaveService.GetByMedicalRecordId(medicalRecordId);
                if (sickLeave == null)
                    return NotFound("Sick leave not found for this medical record");

                return Ok(sickLeave);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/sickleave/daterange?startDate=2024-01-01&endDate=2024-12-31
        [HttpGet("daterange")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<SickLeaveListItemDto>> GetByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            try
            {
                var sickLeaves = _sickLeaveService.GetByDateRange(startDate, endDate);
                return Ok(sickLeaves);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/sickleave
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Create([FromBody] SickLeaveCreateDto dto)
        {
            _logger.LogDebug("Rozpoczęto tworzenie nowego zwolnienia lekarskiego");
            try
            {
                var id = _sickLeaveService.Create(dto);
                _logger.LogDebug($"Zakończono tworzenie zwolnienia lekarskiego o id {id}");
                return CreatedAtAction(nameof(GetById), new { id }, id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Błąd podczas tworzenia zwolnienia lekarskiego: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        // PUT api/sickleave
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Update([FromBody] SickLeaveUpdateDto dto)
        {
            _logger.LogDebug($"Rozpoczęto aktualizację zwolnienia lekarskiego o id {dto.SickLeaveId}");
            try
            {
                _sickLeaveService.Update(dto);
                _logger.LogDebug($"Zakończono aktualizację zwolnienia lekarskiego o id {dto.SickLeaveId}");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Błąd podczas aktualizacji zwolnienia lekarskiego: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/sickleave/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Delete(int id)
        {
            _logger.LogDebug($"Rozpoczęto usuwanie zwolnienia lekarskiego o id {id}");
            try
            {
                _sickLeaveService.Delete(id);
                _logger.LogDebug($"Zakończono usuwanie zwolnienia lekarskiego o id {id}");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Błąd podczas usuwania zwolnienia lekarskiego: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}
