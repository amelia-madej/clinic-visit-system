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

        public VisitController(IVisitService visitService)
        {
            _visitService = visitService;
        }

        // GET api/visit
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<VisitListItemDto>> GetAll()
        {
            var visits = _visitService.GetAll();
            return Ok(visits);
        }

        // GET api/visit/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VisitDetailsDto> GetById(int id)
        {
            var visit = _visitService.GetById(id);
            if (visit == null)
                return NotFound("Visit not found");

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
            try
            {
                var id = _visitService.Create(dto);
                return CreatedAtAction(nameof(GetById), new { id }, id);
            }
            catch (Exception ex)
            {
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
            try
            {
                _visitService.Update(dto);
                return NoContent();
            }
            catch (Exception ex)
            {
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
