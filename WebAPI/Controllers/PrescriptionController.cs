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
        private readonly ILogger<PrescriptionController> _logger;

        public PrescriptionController(
            IPrescriptionService prescriptionService,
            IPrescriptionItemService prescriptionItemService,
            ILogger<PrescriptionController> logger)
        {
            _prescriptionService = prescriptionService;
            _prescriptionItemService = prescriptionItemService;
            _logger = logger;
        }

        // GET api/prescription
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<PrescriptionListItemDto>> GetAll()
        {
            _logger.LogDebug("Started retrieving the list of all prescriptions");
            var prescriptions = _prescriptionService.GetAll();
            _logger.LogDebug("Completed retrieving the list of all prescriptions");
            return Ok(prescriptions);
        }

        // GET api/prescription/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PrescriptionDetailsDto> GetById(int id)
        {
            _logger.LogDebug("Started retrieving prescription with id {Id}", id);
            try
            {
                var prescription = _prescriptionService.GetById(id);
                if (prescription == null)
                {
                    _logger.LogError($"Prescription with id {id} not found");
                    return NotFound("Prescription not found");
                }

                _logger.LogDebug("Completed retrieving prescription with id {Id}", id);
                return Ok(prescription);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving prescription with id {Id}", id);
                return BadRequest(ex.Message);
            }
        }

        // GET api/prescription/medicalrecord/{medicalRecordId}
        [HttpGet("medicalrecord/{medicalRecordId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<PrescriptionListItemDto>> GetByMedicalRecordId(int medicalRecordId)
        {
            _logger.LogDebug("Started retrieving prescriptions by medical record id {MedicalRecordId}", medicalRecordId);
            try
            {
                var prescriptions = _prescriptionService.GetByMedicalRecordId(medicalRecordId);
                _logger.LogDebug("Completed retrieving prescriptions by medical record id {MedicalRecordId}", medicalRecordId);
                return Ok(prescriptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving prescriptions by medical record id {MedicalRecordId}", medicalRecordId);
                return BadRequest(ex.Message);
            }
        }

        // GET api/prescription/expired
        [HttpGet("expired")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<PrescriptionListItemDto>> GetExpired()
        {
            _logger.LogDebug("Started retrieving expired prescriptions");
            var prescriptions = _prescriptionService.GetExpired();
            _logger.LogDebug("Completed retrieving expired prescriptions");
            return Ok(prescriptions);
        }

        // GET api/prescription/expiring-soon?days=7
        [HttpGet("expiring-soon")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<PrescriptionListItemDto>> GetExpiringSoon([FromQuery] int days = 7)
        {
            _logger.LogDebug("Started retrieving prescriptions expiring within {Days} days", days);
            try
            {
                var prescriptions = _prescriptionService.GetExpiringSoon(days);
                _logger.LogDebug("Completed retrieving prescriptions expiring within {Days} days", days);
                return Ok(prescriptions);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving prescriptions expiring within {Days} days", days);
                return BadRequest(ex.Message);
            }
        }

        // POST api/prescription
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Create([FromQuery] int medicalRecordId, [FromBody] PrescriptionCreateDto dto)
        {
            _logger.LogDebug("Started creating a new prescription for medical record {MedicalRecordId}", medicalRecordId);
            try
            {
                var id = _prescriptionService.Create(dto, medicalRecordId);
                _logger.LogDebug("Completed creating prescription with id {Id}", id);
                return CreatedAtAction(nameof(GetById), new { id }, id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating prescription for medical record {MedicalRecordId}", medicalRecordId);
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
            _logger.LogDebug("Started updating prescription with id {Id}", id);
            try
            {
                _prescriptionService.Update(id, dto);
                _logger.LogDebug("Completed updating prescription with id {Id}", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating prescription with id {Id}", id);
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
            _logger.LogDebug("Started deleting prescription with id {Id}", id);
            try
            {
                _prescriptionService.Delete(id);
                _logger.LogDebug("Completed deleting prescription with id {Id}", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting prescription with id {Id}", id);
                return BadRequest(ex.Message);
            }
        }

        // POST api/prescription/{prescriptionId}/items
        [HttpPost("{prescriptionId}/items")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult AddItem(int prescriptionId, [FromBody] PrescriptionItemCreateDto dto)
        {
            _logger.LogDebug("Started adding prescription item to prescription id {PrescriptionId}", prescriptionId);
            try
            {
                var id = _prescriptionItemService.Create(dto, prescriptionId);
                _logger.LogDebug("Completed adding prescription item with id {Id}", id);
                return CreatedAtAction("GetItem", new { id }, id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while adding prescription item to prescription id {PrescriptionId}", prescriptionId);
                return BadRequest(ex.Message);
            }
        }

        // GET api/prescription/{prescriptionId}/items
        [HttpGet("{prescriptionId}/items")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<PrescriptionItemDto>> GetItems(int prescriptionId)
        {
            _logger.LogDebug("Started retrieving items for prescription id {PrescriptionId}", prescriptionId);
            try
            {
                var items = _prescriptionItemService.GetByPrescriptionId(prescriptionId);
                _logger.LogDebug("Completed retrieving items for prescription id {PrescriptionId}", prescriptionId);
                return Ok(items);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving items for prescription id {PrescriptionId}", prescriptionId);
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
            _logger.LogDebug("Started retrieving prescription item with id {ItemId}", itemId);
            try
            {
                var item = _prescriptionItemService.GetById(itemId);
                if (item == null)
                {
                    _logger.LogWarning("Prescription item with id {ItemId} not found", itemId);
                    return NotFound("Prescription item not found");
                }

                _logger.LogDebug("Completed retrieving prescription item with id {ItemId}", itemId);
                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving prescription item with id {ItemId}", itemId);
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
            _logger.LogDebug("Started updating prescription item with id {ItemId}", itemId);
            try
            {
                _prescriptionItemService.Update(itemId, dto);
                _logger.LogDebug("Completed updating prescription item with id {ItemId}", itemId);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating prescription item with id {ItemId}", itemId);
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
            _logger.LogDebug("Started deleting prescription item with id {ItemId}", itemId);
            try
            {
                _prescriptionItemService.Delete(itemId);
                _logger.LogDebug("Completed deleting prescription item with id {ItemId}", itemId);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting prescription item with id {ItemId}", itemId);
                return BadRequest(ex.Message);
            }
        }
    }
}

