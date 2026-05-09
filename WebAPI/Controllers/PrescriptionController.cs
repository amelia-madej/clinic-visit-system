using Application.Services;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.DTOs;
using System;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PrescriptionController : ControllerBase
    {
        private readonly IPrescriptionService _prescriptionService;
        private readonly IPrescriptionItemService _prescriptionItemService;

        public PrescriptionController(IPrescriptionService prescriptionService, IPrescriptionItemService prescriptionItemService)
        {
            _prescriptionService = prescriptionService;
            _prescriptionItemService = prescriptionItemService;
        }

        // GET api/prescription
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<PrescriptionListItemDto>> GetAll()
        {
            var prescriptions = _prescriptionService.GetAll();
            return Ok(prescriptions);
        }

        // GET api/prescription/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PrescriptionDetailsDto> GetById(int id)
        {
            try
            {
                var prescription = _prescriptionService.GetById(id);
                if (prescription == null)
                    return NotFound("Prescription not found");

                return Ok(prescription);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/prescription/medicalrecord/{medicalRecordId}
        [HttpGet("medicalrecord/{medicalRecordId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<PrescriptionListItemDto>> GetByMedicalRecordId(int medicalRecordId)
        {
            try
            {
                var prescriptions = _prescriptionService.GetByMedicalRecordId(medicalRecordId);
                return Ok(prescriptions);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/prescription/expired
        [HttpGet("expired")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<PrescriptionListItemDto>> GetExpired()
        {
            var prescriptions = _prescriptionService.GetExpired();
            return Ok(prescriptions);
        }

        // GET api/prescription/expiring-soon?days=7
        [HttpGet("expiring-soon")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<PrescriptionListItemDto>> GetExpiringSoon([FromQuery] int days = 7)
        {
            try
            {
                var prescriptions = _prescriptionService.GetExpiringSoon(days);
                return Ok(prescriptions);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/prescription
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Create([FromQuery] int medicalRecordId, [FromBody] PrescriptionCreateDto dto)
        {
            try
            {
                var id = _prescriptionService.Create(dto, medicalRecordId);
                return CreatedAtAction(nameof(GetById), new { id }, id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/prescription/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Update(int id, [FromBody] PrescriptionCreateDto dto)
        {
            try
            {
                _prescriptionService.Update(id, dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/prescription/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Delete(int id)
        {
            try
            {
                _prescriptionService.Delete(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/prescription/{prescriptionId}/items
        [HttpPost("{prescriptionId}/items")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult AddItem(int prescriptionId, [FromBody] PrescriptionItemCreateDto dto)
        {
            try
            {
                var id = _prescriptionItemService.Create(dto, prescriptionId);
                return CreatedAtAction("GetItem", new { id }, id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/prescription/{prescriptionId}/items
        [HttpGet("{prescriptionId}/items")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<PrescriptionItemDto>> GetItems(int prescriptionId)
        {
            try
            {
                var items = _prescriptionItemService.GetByPrescriptionId(prescriptionId);
                return Ok(items);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/prescription/items/{itemId}
        [HttpGet("items/{itemId}", Name = "GetItem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PrescriptionItemDto> GetItem(int itemId)
        {
            try
            {
                var item = _prescriptionItemService.GetById(itemId);
                if (item == null)
                    return NotFound("Prescription item not found");

                return Ok(item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/prescription/items/{itemId}
        [HttpPut("items/{itemId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult UpdateItem(int itemId, [FromBody] PrescriptionItemCreateDto dto)
        {
            try
            {
                _prescriptionItemService.Update(itemId, dto);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/prescription/items/{itemId}
        [HttpDelete("items/{itemId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult DeleteItem(int itemId)
        {
            try
            {
                _prescriptionItemService.Delete(itemId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
