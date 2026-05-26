using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace WebAPI.Services;

public class JwtAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public const string SchemeName = "ClinicJwt";
    private readonly IJwtTokenService _jwtTokenService;

    public JwtAuthenticationHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder,
        IJwtTokenService jwtTokenService)
        : base(options, logger, encoder)
    {
        _jwtTokenService = jwtTokenService;
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var authorizationHeader = Request.Headers.Authorization.ToString();
        if (!authorizationHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
            return Task.FromResult(AuthenticateResult.NoResult());

        var token = authorizationHeader["Bearer ".Length..].Trim();
        var principal = _jwtTokenService.ValidateToken(token);

        if (principal is null)
            return Task.FromResult(AuthenticateResult.Fail("Invalid or expired token."));

        var identity = principal.Identity as ClaimsIdentity;
        if (identity is not null && string.IsNullOrWhiteSpace(identity.AuthenticationType))
        {
            identity = new ClaimsIdentity(identity.Claims, SchemeName);
            principal = new ClaimsPrincipal(identity);
        }

        return Task.FromResult(AuthenticateResult.Success(new AuthenticationTicket(principal, SchemeName)));
    }
}
