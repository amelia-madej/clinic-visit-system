using Application.Services;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.DTOs;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MedicationController : Controller
    {
        private readonly IMedicationService _medicationService;
        private readonly IMedicationImportService _medicationImportService;
        private readonly ILogger<MedicationController> _logger;

        public MedicationController(IMedicationService medicationService, IMedicationImportService medicationImportService, ILogger<MedicationController> logger)
        {
            _medicationService = medicationService;
            _medicationImportService = medicationImportService;
            _logger = logger;
        }

        // GET api/medication
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<MedicationDto>> GetAll()
        {
            _logger.LogDebug("Started retrieving the list of all medications");
            var medications = _medicationService.GetAll();
            _logger.LogDebug("Completed retrieving the list of all medications");
            return Ok(medications);
        }

        // GET api/medication/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<MedicationDto> GetById(int id)
        {
            try
            {
                _logger.LogDebug($"Started retrieving medication with id {id}");
                var medication = _medicationService.GetById(id);

                if (medication == null)
                {
                    _logger.LogError($"Medication with id {id} not found");
                    return NotFound("Medication not found");
                }

                _logger.LogDebug($"Completed retrieving medication with id {id}");
                return Ok(medication);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving medication with id {id}");
                return BadRequest(ex.Message);
            }
        }

        // GET api/medication/name/{name}
        [HttpGet("name/{name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<MedicationDto>> GetByName(string name)
        {
            _logger.LogDebug("Started retrieving medications by name: {Name}", name);
            try
            {
                var medications = _medicationService.GetByName(name);

                if (medications == null || !medications.Any())
                {
                    _logger.LogWarning("No medications found by name: {Name}", name);
                    return NotFound("No medications found");
                }

                _logger.LogDebug("Completed retrieving medications by name: {Name}", name);
                return Ok(medications);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving medications by name: {Name}", name);
                return BadRequest(ex.Message);
            }
        }

        // GET api/medication/form/{form}
        [HttpGet("form/{form}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<MedicationDto>> GetByForm(string form)
        {
            _logger.LogDebug("Started retrieving medications by form: {Form}", form);
            try
            {
                var medications = _medicationService.GetByForm(form);

                if (medications == null || !medications.Any())
                {
                    _logger.LogWarning("No medications found by form: {Form}", form);
                    return NotFound("No medications found");
                }

                _logger.LogDebug("Completed retrieving medications by form: {Form}", form);
                return Ok(medications);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving medications by form: {Form}", form);
                return BadRequest(ex.Message);
            }
        }

        // GET api/medication/strength/{strengthValue}
        [HttpGet("strength/{strengthValue}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<MedicationDto>> GetByStrength(decimal strengthValue)
        {
            _logger.LogDebug("Started retrieving medications by strength: {StrengthValue}", strengthValue);
            try
            {
                var medications = _medicationService.GetByStrength(strengthValue);

                if (medications == null || !medications.Any())
                {
                    _logger.LogWarning("No medications found by strength: {StrengthValue}", strengthValue);
                    return NotFound("No medications found");
                }

                _logger.LogDebug("Completed retrieving medications by strength: {StrengthValue}", strengthValue);
                return Ok(medications);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving medications by strength: {StrengthValue}", strengthValue);
                return BadRequest(ex.Message);
            }
        }

        // POST api/medication/active-ingredients
        [HttpPost("active-ingredients")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<MedicationDto>> GetByActiveIngredients([FromBody] List<string> activeIngredients)
        {
            _logger.LogDebug("Started retrieving medications by active ingredients. Count: {Count}", activeIngredients.Count);
            try
            {
                var medications = _medicationService.GetByActiveIngredients(activeIngredients);

                if (medications == null || !medications.Any())
                {
                    _logger.LogWarning("No medications found by active ingredients. Count: {Count}", activeIngredients.Count);
                    return NotFound("No medications found");
                }

                _logger.LogDebug("Completed retrieving medications by active ingredients. Count: {Count}", activeIngredients.Count);
                return Ok(medications);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving medications by active ingredients. Count: {Count}", activeIngredients.Count);
                return BadRequest(ex.Message);
            }
        }

        // GET api/medication/doctor/{doctorId}
        [HttpGet("doctor/{doctorId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<MedicationDto>> GetByDoctorId(int doctorId)
        {
            _logger.LogDebug("Started retrieving medications by doctor id {DoctorId}", doctorId);
            try
            {
                var medications = _medicationService.GetByDoctorId(doctorId);

                if (medications == null || !medications.Any())
                {
                    _logger.LogWarning("No medications found by doctor id {DoctorId}", doctorId);
                    return NotFound("No medications found");
                }

                _logger.LogDebug("Completed retrieving medications by doctor id {DoctorId}", doctorId);
                return Ok(medications);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving medications by doctor id {DoctorId}", doctorId);
                return BadRequest(ex.Message);
            }
        }

        // GET api/medication/patient/{patientId}
        [HttpGet("patient/{patientId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<MedicationDto>> GetByPatientId(int patientId)
        {
            _logger.LogDebug("Started retrieving medications by patient id {PatientId}", patientId);
            try
            {
                var medications = _medicationService.GetByPatientId(patientId);

                if (medications == null || !medications.Any())
                {
                    _logger.LogWarning("No medications found by patient id {PatientId}", patientId);
                    return NotFound("No medications found");
                }

                _logger.LogDebug("Completed retrieving medications by patient id {PatientId}", patientId);
                return Ok(medications);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving medications by patient id {PatientId}", patientId);
                return BadRequest(ex.Message);
            }
        }

        // GET api/medication/prescription/{prescriptionId}
        [HttpGet("prescription/{prescriptionId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<MedicationDto>> GetByPrescriptionId(int prescriptionId)
        {
            _logger.LogDebug("Started retrieving medications by prescription id {PrescriptionId}", prescriptionId);
            try
            {
                var medications = _medicationService.GetByPrescriptionId(prescriptionId);

                if (medications == null || !medications.Any())
                {
                    _logger.LogWarning("No medications found by prescription id {PrescriptionId}", prescriptionId);
                    return NotFound("No medications found");
                }

                _logger.LogDebug("Completed retrieving medications by prescription id {PrescriptionId}", prescriptionId);
                return Ok(medications);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving medications by prescription id {PrescriptionId}", prescriptionId);
                return BadRequest(ex.Message);
            }
        }

        // POST api/medication/import
        [HttpPost("import")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Import()
        {
            _logger.LogInformation("Started importing medications.");
            try
            {
                var count = await _medicationImportService.ImportAsync();
                _logger.LogInformation("Completed importing medications. Imported count: {Count}", count);
                return Ok(new { imported = count });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error importing medications");
                return BadRequest(ex.Message);
            }
        }

        // GET api/medication/visit/{visitId}
        [HttpGet("visit/{visitId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<MedicationDto>> GetByVisitId(int visitId)
        {
            _logger.LogDebug("Started retrieving medications by visit id {VisitId}", visitId);
            try
            {
                var medications = _medicationService.GetByVisitId(visitId);

                if (medications == null || !medications.Any())
                {
                    _logger.LogWarning("No medications found by visit id {VisitId}", visitId);
                    return NotFound("No medications found");
                }

                _logger.LogDebug("Completed retrieving medications by visit id {VisitId}", visitId);
                return Ok(medications);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving medications by visit id {VisitId}", visitId);
                return BadRequest(ex.Message);
            }
        }
    }
}
