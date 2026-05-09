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
            try
            {
                var id = _doctorService.Create(dto);

                return CreatedAtAction(nameof(GetById), new { id }, id);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // api/doctors
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Update(int id, [FromBody] DoctorUpdateDto dto)
        {
            try
            {
                if (id != dto.DoctorId)
                {
                    throw new Exception("Id param is not valid");
                }

                _doctorService.Update(dto);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // api/doctors/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int id)
        {
            try
            {
                _doctorService.Delete(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
