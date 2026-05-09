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
            _logger.LogDebug("Rozpoczęto pobieranie listy wszystkich wizyt");
            var visits = _visitService.GetAll();
            _logger.LogDebug("Zakończono pobieranie listy wszystkich wizyt");
            return Ok(visits);
        }

        // GET api/visit/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VisitDetailsDto> GetById(int id)
        {
            _logger.LogDebug($"Rozpoczęto pobieranie wizyty o id {id}");
            var visit = _visitService.GetById(id);
            if (visit == null)
            {
                _logger.LogError($"Visit with id {id} not found");
                return NotFound("Visit not found");
            }

            _logger.LogDebug($"Zakończono pobieranie wizyty o id {id}");
            return Ok(visit);
        }

        // GET api/visit/patient/{patientId}
        [HttpGet("patient/{patientId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<VisitListItemDto>> GetByPatientId(int patientId)
        {
            try
            {
                var visits = _visitService.GetByPatientId(patientId);
                return Ok(visits);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/visit/doctor/{doctorId}
        [HttpGet("doctor/{doctorId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<VisitListItemDto>> GetByDoctorId(int doctorId)
        {
            try
            {
                var visits = _visitService.GetByDoctorId(doctorId);
                return Ok(visits);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/visit/daterange?startDate=2024-01-01&endDate=2024-12-31
        [HttpGet("daterange")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<VisitListItemDto>> GetByDateRange([FromQuery] DateTime startDate, [FromQuery] DateTime endDate)
        {
            try
            {
                var visits = _visitService.GetByDateRange(startDate, endDate);
                return Ok(visits);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/visit
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Create([FromBody] VisitCreateDto dto)
        {
            _logger.LogDebug("Rozpoczęto tworzenie nowej wizyty");
            try
            {
                var id = _visitService.Create(dto);
                _logger.LogDebug($"Zakończono tworzenie wizyty o id {id}");
                return CreatedAtAction(nameof(GetById), new { id }, id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Błąd podczas tworzenia wizyty: {ex.Message}");
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
            _logger.LogDebug($"Rozpoczęto aktualizację wizyty o id {dto.VisitId}");
            try
            {
                _visitService.Update(dto);
                _logger.LogDebug($"Zakończono aktualizację wizyty o id {dto.VisitId}");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Błąd podczas aktualizacji wizyty: {ex.Message}");
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
            try
            {
                _visitService.CompleteVisit(id, dto);
                return NoContent();
            }
            catch (Exception ex)
            {
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
            try
            {
                _visitService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
