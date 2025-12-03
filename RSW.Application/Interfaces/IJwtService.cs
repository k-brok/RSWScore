using System.Security.Claims;

namespace RSW.Application.Interfaces;

public interface IJwtService
{
    string GenerateToken(string username, IEnumerable<string>? roles = null);
    ClaimsPrincipal? ValidateToken(string token);
}
