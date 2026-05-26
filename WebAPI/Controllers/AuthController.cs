using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.DTOs;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, IJwtTokenService jwtTokenService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _jwtTokenService = jwtTokenService;
            _logger = logger;
        }

        [HttpPost("login")]
        public IActionResult Login(LoginDto dto)
        {
            _logger.LogInformation("Login attempt for email: {Email}", dto.Email);

            try
            {
                var result = _authService.Login(dto);
                _jwtTokenService.AddToken(result);
                _logger.LogInformation("Login successful for user id: {UserId}", result.UserId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex, "Login failed for email: {Email}", dto.Email);
                return Unauthorized(new
                {
                    message = ex.Message
                });
            }
        }
    }
}
