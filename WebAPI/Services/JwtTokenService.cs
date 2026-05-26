using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using SharedKernel.DTOs;

namespace WebAPI.Services;

public class JwtTokenService : IJwtTokenService
{
    private readonly IConfiguration _configuration;

    public JwtTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public AuthResponseDto AddToken(AuthResponseDto auth)
    {
        var expiresAt = DateTime.UtcNow.AddMinutes(GetExpirationMinutes());
        var credentials = new SigningCredentials(GetSigningKey(), SecurityAlgorithms.HmacSha256);
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, auth.UserId.ToString()),
            new(ClaimTypes.NameIdentifier, auth.UserId.ToString()),
            new(ClaimTypes.Name, auth.FullName),
            new(ClaimTypes.Email, auth.Email),
            new(ClaimTypes.Role, auth.Role.ToString())
        };

        if (auth.PatientId is int patientId)
            claims.Add(new Claim("patientId", patientId.ToString()));

        if (auth.DoctorId is int doctorId)
            claims.Add(new Claim("doctorId", doctorId.ToString()));

        var token = new JwtSecurityToken(
            issuer: GetIssuer(),
            audience: GetAudience(),
            claims: claims,
            expires: expiresAt,
            signingCredentials: credentials);

        auth.Token = new JwtSecurityTokenHandler().WriteToken(token);
        auth.TokenExpiresAtUtc = expiresAt;

        return auth;
    }

    public ClaimsPrincipal? ValidateToken(string token)
    {
        if (string.IsNullOrWhiteSpace(token))
            return null;

        var handler = new JwtSecurityTokenHandler();

        try
        {
            return handler.ValidateToken(token, GetValidationParameters(), out _);
        }
        catch
        {
            return null;
        }
    }

    private TokenValidationParameters GetValidationParameters() => new()
    {
        ValidateIssuer = true,
        ValidIssuer = GetIssuer(),
        ValidateAudience = true,
        ValidAudience = GetAudience(),
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = GetSigningKey(),
        ValidateLifetime = true,
        ClockSkew = TimeSpan.FromMinutes(2)
    };

    private SymmetricSecurityKey GetSigningKey()
    {
        var key = _configuration["Jwt:SecretKey"];
        if (string.IsNullOrWhiteSpace(key) || key.Length < 32)
            throw new InvalidOperationException("JWT secret key must contain at least 32 characters.");

        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
    }

    private string GetIssuer() => _configuration["Jwt:Issuer"] ?? "ClinicVisitSystem";

    private string GetAudience() => _configuration["Jwt:Audience"] ?? "ClinicVisitSystem.Client";

    private int GetExpirationMinutes()
    {
        var configuredValue = _configuration.GetValue<int?>("Jwt:ExpirationMinutes");
        return configuredValue is > 0 ? configuredValue.Value : 120;
    }
}
