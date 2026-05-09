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
            _logger.LogDebug("Rozpoczęto pobieranie listy wszystkich pacjentów");
            var patients = _patientService.GetAll();
            _logger.LogDebug("Zakończono pobieranie listy wszystkich pacjentów");
            return Ok(patients);
        }

        // GET api/patient/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PatientDetailsDto> GetById(int id)
        {
            _logger.LogDebug($"Rozpoczęto pobieranie pacjenta o id {id}");
            var patient = _patientService.GetById(id);

            if (patient == null)
            {
                _logger.LogError($"Patient with id {id} not found");
                return NotFound("Patient not found");
            }

            _logger.LogDebug($"Zakończono pobieranie pacjenta o id {id}");
            return Ok(patient);
        }

        // GET api/patient/email/{email}
        [HttpGet("email/{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PatientDetailsDto> GetByEmail(string email)
        {
            var patient = _patientService.GetByEmail(email);

            if (patient == null)
                return NotFound("Patient not found");

            return Ok(patient);
        }

        // GET api/patient/pesel/{pesel}
        [HttpGet("pesel/{pesel}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PatientDetailsDto> GetByPesel(string pesel)
        {
            var patient = _patientService.GetByPesel(pesel);

            if (patient == null)
                return NotFound("Patient not found");

            return Ok(patient);
        }

        // GET api/patient/phone/{phoneNumber}
        [HttpGet("phone/{phoneNumber}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PatientDetailsDto> GetByPhoneNumber(string phoneNumber)
        {
            var patient = _patientService.GetByPhoneNumber(phoneNumber);

            if (patient == null)
                return NotFound("Patient not found");

            return Ok(patient);
        }

        // POST api/patient
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Create([FromBody] PatientCreateDto dto)
        {
            try
            {
                var id = _patientService.Create(dto);

                return CreatedAtAction(nameof(GetById), new { id }, id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/patient
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Update(int id, [FromBody] PatientUpdateDto dto)
        {
            try
            {
                if (id != dto.PatientId)
                    throw new Exception("Id param is not valid");

                _patientService.Update(dto);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/patient/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int id)
        {
            try
            {
                _patientService.Delete(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
