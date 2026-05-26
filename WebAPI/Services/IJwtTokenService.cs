using System.Security.Claims;
using SharedKernel.DTOs;

namespace WebAPI.Services;

public interface IJwtTokenService
{
    AuthResponseDto AddToken(AuthResponseDto auth);
    ClaimsPrincipal? ValidateToken(string token);
}
