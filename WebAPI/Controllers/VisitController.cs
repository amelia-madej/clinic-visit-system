using Application.Services;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.DTOs;
using System;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VisitController : ControllerBase
    {
        private readonly IVisitService _visitService;
        private readonly ILogger<VisitController> _logger;

        public VisitController(
            IVisitService visitService,
            ILogger<VisitController> logger)
        {
            _visitService = visitService;
            _logger = logger;
        }

        // GET api/visit
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VisitListItemDto>> GetAll()
        {
            _logger.LogDebug("Started retrieving the list of all visits");
            var visits = _visitService.GetAll();
            _logger.LogDebug("Completed retrieving the list of all visits");
            return Ok(visits);
        }

        // GET api/visit/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VisitDetailsDto> GetById(int id)
        {
            _logger.LogDebug("Started retrieving visit with id {Id}", id);
            var visit = _visitService.GetById(id);
            if (visit == null)
            {
                _logger.LogError($"Visit with id {id} not found");
                return NotFound("Visit not found");
            }

            _logger.LogDebug("Completed retrieving visit with id {Id}", id);
            return Ok(visit);
        }

        // GET api/visit/patient/{patientId}
        [HttpGet("patient/{patientId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<VisitListItemDto>> GetByPatientId(int patientId)
        {
            _logger.LogDebug("Started retrieving visits by patient id {PatientId}", patientId);
            try
            {
                var visits = _visitService.GetByPatientId(patientId);
                _logger.LogDebug("Completed retrieving visits by patient id {PatientId}", patientId);
                return Ok(visits);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving visits by patient id {PatientId}", patientId);
                return BadRequest(ex.Message);
            }
        }

        // GET api/visit/doctor/{doctorId}
        [HttpGet("doctor/{doctorId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<VisitListItemDto>> GetByDoctorId(int doctorId)
        {
            _logger.LogDebug("Started retrieving visits by doctor id {DoctorId}", doctorId);
            try
            {
                var visits = _visitService.GetByDoctorId(doctorId);
                _logger.LogDebug("Completed retrieving visits by doctor id {DoctorId}", doctorId);
                return Ok(visits);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving visits by doctor id {DoctorId}", doctorId);
                return BadRequest(ex.Message);
            }
        }

        // GET api/visit/daterange?startDate=2024-01-01&endDate=2024-12-31
        [HttpGet("daterange")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<VisitListItemDto>> GetByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            _logger.LogDebug("Started retrieving visits between {StartDate} and {EndDate}", startDate, endDate);
            try
            {
                var visits = _visitService.GetByDateRange(startDate, endDate);
                _logger.LogDebug("Completed retrieving visits between {StartDate} and {EndDate}", startDate, endDate);
                return Ok(visits);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving visits between {StartDate} and {EndDate}", startDate, endDate);
                return BadRequest(ex.Message);
            }
        }

        // POST api/visit
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Create([FromBody] VisitCreateDto dto)
        {
            _logger.LogDebug("Started creating a new visit");
            try
            {
                var id = _visitService.Create(dto);
                _logger.LogDebug("Completed creating visit with id {Id}", id);
                return CreatedAtAction(nameof(GetById), new { id }, id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating visit");
                return BadRequest(ex.Message);
            }
        }

        // PUT api/visit
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Update([FromBody] VisitUpdateDto dto)
        {
            _logger.LogDebug("Started updating visit with id {Id}", dto.VisitId);
            try
            {
                _visitService.Update(dto);
                _logger.LogDebug("Completed updating visit with id {Id}", dto.VisitId);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating visit with id {Id}", dto.VisitId);
                return BadRequest(ex.Message);
            }
        }

        // POST api/visit/{id}/complete
        [HttpPost("{id}/complete")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult CompleteVisit(int id, [FromBody] VisitCompleteDto dto)
        {
            _logger.LogDebug("Started completing visit with id {Id}", id);
            try
            {
                _visitService.CompleteVisit(id, dto);
                _logger.LogDebug("Completed visit with id {Id}", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while completing visit with id {Id}", id);
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/visit/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Delete(int id)
        {
            _logger.LogDebug("Started deleting visit with id {Id}", id);
            try
            {
                _visitService.Delete(id);
                _logger.LogDebug("Completed deleting visit with id {Id}", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting visit with id {Id}", id);
                return BadRequest(ex.Message);
            }
        }
    }
}

