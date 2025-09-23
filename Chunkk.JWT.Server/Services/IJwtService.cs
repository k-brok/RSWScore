using System.Security.Claims;

namespace Chunkk.JWT.Server.Services;

public interface IJwtService
{
    string GenerateToken(string username, IEnumerable<string>? roles = null);
    ClaimsPrincipal? ValidateToken(string token);
}
