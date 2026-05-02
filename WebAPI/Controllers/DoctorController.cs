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

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<DoctorListItemDto>> GetAll()
        {
            var doctors = _doctorService.GetAll();
            return Ok(doctors);
        }

        //api/doctors/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<DoctorDetailsDto> GetById(int id)
        {
            var doctor = _doctorService.GetById(id);

            if (doctor == null)
                return NotFound("Doctor not found");

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
