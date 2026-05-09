using Application.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using SharedKernel.DTOs;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        // GET api/user
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<UserDto>> GetAll()
        {
            _logger.LogDebug("Rozpoczęto pobieranie listy wszystkich użytkowników");
            var users = _userService.GetAll();
            _logger.LogDebug("Zakończono pobieranie listy wszystkich użytkowników");
            return Ok(users);
        }

        // GET api/user/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UserDto> GetById(int id)
        {
            try
            {
                _logger.LogDebug($"Rozpoczęto pobieranie użytkownika o id {id}");
                var user = _userService.GetById(id);

                if (user == null)
                {
                    _logger.LogError($"User with id {id} not found");
                    return NotFound("User not found");
                }

                _logger.LogDebug($"Zakończono pobieranie użytkownika o id {id}");
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving user with id {id}");
                return BadRequest(ex.Message);
            }
        }

        // GET api/user/email/{email}
        [HttpGet("email/{email}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<UserDto> GetByEmail(string email)
        {
            try
            {
                var user = _userService.GetByEmail(email);

                if (user == null)
                    return NotFound("User not found");

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/user/phone/{phoneNumber}
        [HttpGet("phone/{phoneNumber}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<UserDto> GetByPhoneNumber(string phoneNumber)
        {
            try
            {
                var user = _userService.GetByPhoneNumber(phoneNumber);

                if (user == null)
                    return NotFound("User not found");

                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/user/role/{role}
        [HttpGet("role/{role}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<IEnumerable<UserDto>> GetByRole(UserRole role)
        {
            try
            {
                var users = _userService.GetByRole(role);

                if (users == null || !users.Any())
                    return NotFound("No users found");

                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/user
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Create([FromBody] CreateUserDto dto)
        {
            _logger.LogDebug("Rozpoczęto tworzenie nowego użytkownika");
            try
            {
                var id = _userService.Create(dto);
                _logger.LogDebug($"Zakończono tworzenie użytkownika o id {id}");
                return CreatedAtAction(nameof(GetById), new { id }, id);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Błąd podczas tworzenia użytkownika: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        // PUT api/user/{id}
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Update(int id, [FromBody] UpdateUserDto dto)
        {
            _logger.LogDebug($"Rozpoczęto aktualizację użytkownika o id {id}");
            try
            {
                if (id != dto.UserId)
                {
                    _logger.LogError($"Id param is not valid: {id} != {dto.UserId}");
                    throw new Exception("Id param is not valid");
                }

                _userService.Update(dto);
                _logger.LogDebug($"Zakończono aktualizację użytkownika o id {id}");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Błąd podczas aktualizacji użytkownika: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/user/{id}
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Delete(int id)
        {
            _logger.LogDebug($"Rozpoczęto usuwanie użytkownika o id {id}");
            try
            {
                _userService.Delete(id);
                _logger.LogDebug($"Zakończono usuwanie użytkownika o id {id}");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Błąd podczas usuwania użytkownika: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}