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
            _logger.LogDebug("Rozpoczęto pobieranie listy wszystkich lekarzy");
            var doctors = _doctorService.GetAll();
            _logger.LogDebug("Zakończono pobieranie listy wszystkich lekarzy");
            return Ok(doctors);
        }

        //api/doctors/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<DoctorDetailsDto> GetById(int id)
        {
            _logger.LogDebug($"Rozpoczęto pobieranie lekarza o id {id}");
            var doctor = _doctorService.GetById(id);

            if (doctor == null)
            {
                _logger.LogError($"Doctor with id {id} not found");
                return NotFound("Doctor not found");
            }

            _logger.LogDebug($"Zakończono pobieranie lekarza o id {id}");
            return Ok(doctor);
        }

        // api/doctors/lastname/{lastName}
        [HttpGet("lastname/{lastName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<DoctorListItemDto>> GetByLastName(string lastName)
        {
            var doctors = _doctorService.GetDoctorsByLastName(lastName);

            if (doctors == null || !doctors.Any())
                return NotFound("Doctor not found");

            return Ok(doctors);
        }

        // api/doctors/specialization/{specialization}
        [HttpGet("specialization/{specialization}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<DoctorListItemDto>> GetBySpecialization(string specialization)
        {
            var doctors = _doctorService.GetDoctorsBySpecialization(specialization);

            if (doctors == null || !doctors.Any())
                return NotFound("Doctor not found");

            return Ok(doctors);
        }

        //api/doctors
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Create([FromBody] DoctorCreateDto dto)
        {
            _logger.LogDebug("Rozpoczęto tworzenie nowego lekarza");
            try
            {
                var id = _doctorService.Create(dto);
                _logger.LogDebug($"Zakończono tworzenie lekarza o id {id}");
                return CreatedAtAction(nameof(GetById), new { id }, id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Błąd podczas tworzenia lekarza: {ex.Message}");
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
            _logger.LogDebug($"Rozpoczęto aktualizację lekarza o id {id}");
            try
            {
                if (id != dto.DoctorId)
                {
                    _logger.LogError($"Id param is not valid: {id} != {dto.DoctorId}");
                    throw new Exception("Id param is not valid");
                }

                _doctorService.Update(dto);
                _logger.LogDebug($"Zakończono aktualizację lekarza o id {id}");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Błąd podczas aktualizacji lekarza: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        // api/doctors/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int id)
        {
            _logger.LogDebug($"Rozpoczęto usuwanie lekarza o id {id}");
            try
            {
                _doctorService.Delete(id);
                _logger.LogDebug($"Zakończono usuwanie lekarza o id {id}");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Błąd podczas usuwania lekarza: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}
