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

        public MedicationController(IMedicationService medicationService)
        {
            _medicationService = medicationService;
        }

        // GET api/medication
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<MedicationDto>> GetAll()
        {
            var medications = _medicationService.GetAll();
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
                var medication = _medicationService.GetById(id);

                if (medication == null)
                    return NotFound("Medication not found");

                return Ok(medication);
            }
            catch (Exception ex)
            {
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
            try
            {
                var medications = _medicationService.GetByName(name);

                if (medications == null || !medications.Any())
                    return NotFound("No medications found");

                return Ok(medications);
            }
            catch (Exception ex)
            {
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
            try
            {
                var medications = _medicationService.GetByForm(form);

                if (medications == null || !medications.Any())
                    return NotFound("No medications found");

                return Ok(medications);
            }
            catch (Exception ex)
            {
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
            try
            {
                var medications = _medicationService.GetByStrength(strengthValue);

                if (medications == null || !medications.Any())
                    return NotFound("No medications found");

                return Ok(medications);
            }
            catch (Exception ex)
            {
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
            try
            {
                var medications = _medicationService.GetByActiveIngredients(activeIngredients);

                if (medications == null || !medications.Any())
                    return NotFound("No medications found");

                return Ok(medications);
            }
            catch (Exception ex)
            {
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
            try
            {
                var medications = _medicationService.GetByDoctorId(doctorId);

                if (medications == null || !medications.Any())
                    return NotFound("No medications found");

                return Ok(medications);
            }
            catch (Exception ex)
            {
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
            try
            {
                var medications = _medicationService.GetByPatientId(patientId);

                if (medications == null || !medications.Any())
                    return NotFound("No medications found");

                return Ok(medications);
            }
            catch (Exception ex)
            {
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
            try
            {
                var medications = _medicationService.GetByPrescriptionId(prescriptionId);

                if (medications == null || !medications.Any())
                    return NotFound("No medications found");

                return Ok(medications);
            }
            catch (Exception ex)
            {
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
            try
            {
                var medications = _medicationService.GetByVisitId(visitId);

                if (medications == null || !medications.Any())
                    return NotFound("No medications found");

                return Ok(medications);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}