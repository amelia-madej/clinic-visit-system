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
            _logger.LogDebug("Started retrieving the list of all users");
            var users = _userService.GetAll();
            _logger.LogDebug("Completed retrieving the list of all users");
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
                _logger.LogDebug($"Started retrieving user with id {id}");
                var user = _userService.GetById(id);

                if (user == null)
                {
                    _logger.LogError($"User with id {id} not found");
                    return NotFound("User not found");
                }

                _logger.LogDebug($"Completed retrieving user with id {id}");
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
            _logger.LogDebug("Started retrieving user by email: {Email}", email);
            try
            {
                var user = _userService.GetByEmail(email);

                if (user == null)
                {
                    _logger.LogWarning("User by email {Email} not found", email);
                    return NotFound("User not found");
                }

                _logger.LogDebug("Completed retrieving user by email: {Email}", email);
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving user by email: {Email}", email);
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
            _logger.LogDebug("Started retrieving user by phone number.");
            try
            {
                var user = _userService.GetByPhoneNumber(phoneNumber);

                if (user == null)
                {
                    _logger.LogWarning("User by phone number not found.");
                    return NotFound("User not found");
                }

                _logger.LogDebug("Completed retrieving user by phone number.");
                return Ok(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving user by phone number.");
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
            _logger.LogDebug("Started retrieving users by role {Role}", role);
            try
            {
                var users = _userService.GetByRole(role);

                if (users == null || !users.Any())
                {
                    _logger.LogWarning("No users found for role {Role}", role);
                    return NotFound("No users found");
                }

                _logger.LogDebug("Completed retrieving users by role {Role}", role);
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while retrieving users by role {Role}", role);
                return BadRequest(ex.Message);
            }
        }

        // POST api/user
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Create([FromBody] CreateUserDto dto)
        {
            _logger.LogDebug("Started creating a new user");
            try
            {
                var id = _userService.Create(dto);
                _logger.LogDebug($"Completed creating user with id {id}");
                return CreatedAtAction(nameof(GetById), new { id }, id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while creating user");
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
            _logger.LogDebug($"Started updating user with id {id}");
            try
            {
                if (id != dto.UserId)
                {
                    _logger.LogError($"Id param is not valid: {id} != {dto.UserId}");
                    throw new Exception("Id param is not valid");
                }

                _userService.Update(dto);
                _logger.LogDebug($"Completed updating user with id {id}");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating user with id {Id}", id);
                return BadRequest(ex.Message);
            }
        }

        // PUT api/user/{id}/profile
        [HttpPut("{id}/profile")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult UpdateProfile(int id, [FromBody] UpdateUserProfileDto dto)
        {
            _logger.LogDebug($"Started updating user profile with id {id}");
            try
            {
                if (id != dto.UserId)
                    throw new Exception("Id param is not valid");

                _userService.UpdateProfile(dto);
                _logger.LogDebug($"Completed updating user profile with id {id}");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating user profile with id {Id}", id);
                return BadRequest(ex.Message);
            }
        }

        // PUT api/user/{id}/photo
        [HttpPut("{id}/photo")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult UpdatePhoto(int id, [FromBody] UpdateUserPhotoDto dto)
        {
            _logger.LogDebug($"Started updating user photo with id {id}");
            try
            {
                if (id != dto.UserId)
                    throw new Exception("Id param is not valid");

                _userService.UpdatePhoto(dto);
                _logger.LogDebug($"Completed updating user photo with id {id}");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while updating user photo with id {Id}", id);
                return BadRequest(ex.Message);
            }
        }

        // PUT api/user/{id}/password
        [HttpPut("{id}/password")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult ChangePassword(int id, [FromBody] ChangePasswordDto dto)
        {
            _logger.LogInformation("Started changing password for user id {Id}", id);
            try
            {
                if (id != dto.UserId)
                    throw new Exception("Id param is not valid");

                _userService.ChangePassword(dto);
                _logger.LogInformation("Completed changing password for user id {Id}", id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Error while changing password for user id {Id}", id);
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/user/{id}/photo
        [HttpDelete("{id}/photo")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult DeletePhoto(int id)
        {
            _logger.LogDebug($"Started deleting user photo with id {id}");
            try
            {
                _userService.DeletePhoto(id);
                _logger.LogDebug($"Completed deleting user photo with id {id}");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting user photo with id {Id}", id);
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
            _logger.LogDebug($"Started deleting user with id {id}");
            try
            {
                _userService.Delete(id);
                _logger.LogDebug($"Completed deleting user with id {id}");
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while deleting user with id {Id}", id);
                return BadRequest(ex.Message);
            }
        }
    }
}

