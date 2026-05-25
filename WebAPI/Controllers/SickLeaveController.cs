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
            _logger.LogDebug("Started retrieving the list of all sick leaves");
            var sickLeaves = _sickLeaveService.GetAll();
            _logger.LogDebug("Completed retrieving the list of all sick leaves");
            return Ok(sickLeaves);
        }

        // GET api/sickleave/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<SickLeaveDetailsDto> GetById(int id)
        {
            _logger.LogDebug("Started retrieving sick leave with id {Id}", id);
            try
            {
                var sickLeave = _sickLeaveService.GetById(id);
                if (sickLeave == null)
                {
                    _logger.LogError($"Sick leave with id {id} not found");
                    return NotFound("Sick leave not found");
                }

                _logger.LogDebug("Completed retrieving sick leave with id {Id}", id);
                return Ok(sickLeave);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving sick leave with id {Id}", id);
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
            _logger.LogDebug("Started retrieving sick leave by medical record id {MedicalRecordId}", medicalRecordId);
            try
            {
                var sickLeave = _sickLeaveService.GetByMedicalRecordId(medicalRecordId);
                if (sickLeave == null)
                {
                    _logger.LogWarning("Sick leave for medical record id {MedicalRecordId} not found", medicalRecordId);
                    return NotFound("Sick leave not found for this medical record");
                }

                _logger.LogDebug("Completed retrieving sick leave by medical record id {MedicalRecordId}", medicalRecordId);
                return Ok(sickLeave);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving sick leave by medical record id {MedicalRecordId}", medicalRecordId);
                return BadRequest(ex.Message);
            }
        }

        // GET api/sickleave/daterange?startDate=2024-01-01&endDate=2024-12-31
        [HttpGet("daterange")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<SickLeaveListItemDto>> GetByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            _logger.LogDebug("Started retrieving sick leaves between {StartDate} and {EndDate}", startDate, endDate);
            try
            {
                var sickLeaves = _sickLeaveService.GetByDateRange(startDate, endDate);
                _logger.LogDebug("Completed retrieving sick leaves between {StartDate} and {EndDate}", startDate, endDate);
                return Ok(sickLeaves);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving sick leaves between {StartDate} and {EndDate}", startDate, endDate);
                return BadRequest(ex.Message);
            }
        }

        // POST api/sickleave
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Create([FromBody] SickLeaveCreateDto dto)
        {
            _logger.LogDebug("Started creating a new sick leave");
            try
            {
                var id = _sickLeaveService.Create(dto);
                _logger.LogDebug("Completed creating sick leave with id {Id}", id);
                return CreatedAtAction(nameof(GetById), new { id }, id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating sick leave");
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
            _logger.LogDebug("Started updating sick leave with id {Id}", dto.SickLeaveId);
            try
            {
                _sickLeaveService.Update(dto);
                _logger.LogDebug("Completed updating sick leave with id {Id}", dto.SickLeaveId);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating sick leave with id {Id}", dto.SickLeaveId);
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
            _logger.LogDebug("Started deleting sick leave with id {Id}", id);
            try
            {
                _sickLeaveService.Delete(id);
                _logger.LogDebug("Completed deleting sick leave with id {Id}", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting sick leave with id {Id}", id);
                return BadRequest(ex.Message);
            }
        }
    }
}

