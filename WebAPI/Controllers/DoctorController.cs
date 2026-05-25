using Application.Services;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.DTOs;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctorController : Controller
    {
        private readonly IDoctorService _doctorService;
        private readonly ILogger<DoctorController> _logger;

        public DoctorController(IDoctorService doctorService, ILogger<DoctorController> logger)
        {
            _doctorService = doctorService;
            _logger = logger;
        }

        [HttpGet]
        public ActionResult<IEnumerable<DoctorListItemDto>> GetAll()
        {
            _logger.LogDebug("Started retrieving the list of all doctors");
            var doctors = _doctorService.GetAll();
            _logger.LogDebug("Completed retrieving the list of all doctors");
            return Ok(doctors);
        }

        //api/doctors/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<DoctorDetailsDto> GetById(int id)
        {
            _logger.LogDebug($"Started retrieving doctor with id {id}");
            var doctor = _doctorService.GetById(id);

            if (doctor == null)
            {
                _logger.LogError($"Doctor with id {id} not found");
                return NotFound("Doctor not found");
            }

            _logger.LogDebug($"Completed retrieving doctor with id {id}");
            return Ok(doctor);
        }

        // api/doctors/lastname/{lastName}
        [HttpGet("lastname/{lastName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<DoctorListItemDto>> GetByLastName(string lastName)
        {
            _logger.LogDebug("Started retrieving doctors by last name: {LastName}", lastName);
            var doctors = _doctorService.GetDoctorsByLastName(lastName);

            if (doctors == null || !doctors.Any())
            {
                _logger.LogWarning("No doctors found by last name: {LastName}", lastName);
                return NotFound("Doctor not found");
            }

            _logger.LogDebug("Completed retrieving doctors by last name: {LastName}", lastName);
            return Ok(doctors);
        }

        // api/doctors/specialization/{specialization}
        [HttpGet("specialization/{specialization}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<DoctorListItemDto>> GetBySpecialization(string specialization)
        {
            _logger.LogDebug("Started retrieving doctors by specialization: {Specialization}", specialization);
            var doctors = _doctorService.GetDoctorsBySpecialization(specialization);

            if (doctors == null || !doctors.Any())
            {
                _logger.LogWarning("No doctors found by specialization: {Specialization}", specialization);
                return NotFound("Doctor not found");
            }

            _logger.LogDebug("Completed retrieving doctors by specialization: {Specialization}", specialization);
            return Ok(doctors);
        }

        //api/doctors
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Create([FromBody] DoctorCreateDto dto)
        {
            _logger.LogDebug("Started creating a new doctor");
            try
            {
                var id = _doctorService.Create(dto);
                _logger.LogDebug($"Completed creating doctor with id {id}");
                return CreatedAtAction(nameof(GetById), new { id }, id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating doctor");
                return BadRequest(ex.Message);
            }
        }

        // api/doctors/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Update(int id, [FromBody] DoctorUpdateDto dto)
        {
            _logger.LogDebug($"Started updating doctor with id {id}");
            try
            {
                if (id != dto.DoctorId)
                {
                    _logger.LogError($"Id param is not valid: {id} != {dto.DoctorId}");
                    throw new Exception("Id param is not valid");
                }

                _doctorService.Update(dto);
                _logger.LogDebug($"Completed updating doctor with id {id}");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating doctor with id {Id}", id);
                return BadRequest(ex.Message);
            }
        }

        // api/doctors/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int id)
        {
            _logger.LogDebug($"Started deleting doctor with id {id}");
            try
            {
                _doctorService.Delete(id);
                _logger.LogDebug($"Completed deleting doctor with id {id}");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting doctor with id {Id}", id);
                return BadRequest(ex.Message);
            }
        }
    }
}

