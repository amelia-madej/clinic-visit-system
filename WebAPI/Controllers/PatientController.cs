using Application.Services;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.DTOs;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientController : Controller
    {
        private readonly IPatientService _patientService;
        private readonly ILogger<PatientController> _logger;
        
        public PatientController(IPatientService patientService, ILogger<PatientController> logger)
        {
            _patientService = patientService;
            _logger = logger;
        }
        // GET api/patient
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<PatientListItemDto>> GetAll()
        {
            _logger.LogDebug("Started retrieving the list of all patients");
            var patients = _patientService.GetAll();
            _logger.LogDebug("Completed retrieving the list of all patients");
            return Ok(patients);
        }

        // GET api/patient/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PatientDetailsDto> GetById(int id)
        {
            _logger.LogDebug($"Started retrieving patient with id {id}");
            var patient = _patientService.GetById(id);

            if (patient == null)
            {
                _logger.LogError($"Patient with id {id} not found");
                return NotFound("Patient not found");
            }

            _logger.LogDebug($"Completed retrieving patient with id {id}");
            return Ok(patient);
        }

        // GET api/patient/email/{email}
        [HttpGet("email/{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PatientDetailsDto> GetByEmail(string email)
        {
            _logger.LogDebug("Started retrieving patient by email: {Email}", email);
            var patient = _patientService.GetByEmail(email);

            if (patient == null)
            {
                _logger.LogWarning("Patient by email {Email} not found", email);
                return NotFound("Patient not found");
            }

            _logger.LogDebug("Completed retrieving patient by email: {Email}", email);
            return Ok(patient);
        }

        // GET api/patient/pesel/{pesel}
        [HttpGet("pesel/{pesel}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PatientDetailsDto> GetByPesel(string pesel)
        {
            _logger.LogDebug("Started retrieving patient by PESEL.");
            var patient = _patientService.GetByPesel(pesel);

            if (patient == null)
            {
                _logger.LogWarning("Patient by PESEL not found.");
                return NotFound("Patient not found");
            }

            _logger.LogDebug("Completed retrieving patient by PESEL.");
            return Ok(patient);
        }

        // GET api/patient/phone/{phoneNumber}
        [HttpGet("phone/{phoneNumber}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PatientDetailsDto> GetByPhoneNumber(string phoneNumber)
        {
            _logger.LogDebug("Started retrieving patient by phone number.");
            var patient = _patientService.GetByPhoneNumber(phoneNumber);

            if (patient == null)
            {
                _logger.LogWarning("Patient by phone number not found.");
                return NotFound("Patient not found");
            }

            _logger.LogDebug("Completed retrieving patient by phone number.");
            return Ok(patient);
        }

        // POST api/patient
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Create([FromBody] PatientCreateDto dto)
        {
            _logger.LogDebug("Started creating a new patient");
            try
            {
                var id = _patientService.Create(dto);
                _logger.LogDebug($"Completed creating patient with id {id}");
                return CreatedAtAction(nameof(GetById), new { id }, id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating patient");
                return BadRequest(ex.Message);
            }
        }

        // PUT api/patient/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Update(int id, [FromBody] PatientUpdateDto dto)
        {
            _logger.LogDebug($"Started updating patient with id {id}");
            try
            {
                if (id != dto.PatientId)
                {
                    _logger.LogError($"Id param is not valid: {id} != {dto.PatientId}");
                    throw new Exception("Id param is not valid");
                }

                _patientService.Update(dto);
                _logger.LogDebug($"Completed updating patient with id {id}");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating patient with id {Id}", id);
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/patient/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int id)
        {
            _logger.LogDebug($"Started deleting patient with id {id}");
            try
            {
                _patientService.Delete(id);
                _logger.LogDebug($"Completed deleting patient with id {id}");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting patient with id {Id}", id);
                return BadRequest(ex.Message);
            }
        }
    }
}

