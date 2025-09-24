using System.Security.Claims;

namespace RSW.API.Services;

public interface IJwtService
{
    string GenerateToken(string username, IEnumerable<string>? roles = null);
    ClaimsPrincipal? ValidateToken(string token);
}
