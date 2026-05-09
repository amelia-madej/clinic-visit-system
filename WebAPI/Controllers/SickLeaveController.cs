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

        public SickLeaveController(ISickLeaveService sickLeaveService)
        {
            _sickLeaveService = sickLeaveService;
        }

        // GET api/sickleave
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<SickLeaveListItemDto>> GetAll()
        {
            var sickLeaves = _sickLeaveService.GetAll();
            return Ok(sickLeaves);
        }

        // GET api/sickleave/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<SickLeaveDetailsDto> GetById(int id)
        {
            try
            {
                var sickLeave = _sickLeaveService.GetById(id);
                if (sickLeave == null)
                    return NotFound("Sick leave not found");

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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<SickLeaveListItemDto>> GetByMedicalRecordId(int medicalRecordId)
        {
            try
            {
                var sickLeaves = _sickLeaveService.GetByMedicalRecordId(medicalRecordId);
                return Ok(sickLeaves);
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
            try
            {
                var id = _sickLeaveService.Create(dto);
                return CreatedAtAction(nameof(GetById), new { id }, id);
            }
            catch (Exception ex)
            {
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
            try
            {
                _sickLeaveService.Update(dto);
                return NoContent();
            }
            catch (Exception ex)
            {
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
            try
            {
                _sickLeaveService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
