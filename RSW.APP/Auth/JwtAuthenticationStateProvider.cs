using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using RSW.APP.Services;
using RSW.Shared.Interfaces;

namespace RSW.APP.Auth;

public class JwtAuthenticationStateProvider : AuthenticationStateProvider
{
    private readonly ITokenStorage _tokenStorage;
    public event Action AuthenticationStateChangedEvent;

    public JwtAuthenticationStateProvider(ITokenStorage tokenStorage)
    {
        _tokenStorage = tokenStorage;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        var token = await _tokenStorage.GetTokenAsync();
        if (string.IsNullOrEmpty(token) || IsTokenExpired(token))
        {
            await _tokenStorage.RemoveTokenAsync();
            return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
        }

        var claims = ParseClaimsFromJwt(token);
        var identity = new ClaimsIdentity(claims, "jwt");
        var user = new ClaimsPrincipal(identity);

        AuthenticationStateChangedEvent?.Invoke();

        return new AuthenticationState(user);
    }

    public void NotifyUserAuthentication(string token)
    {
        var claims = ParseClaimsFromJwt(token);
        var identity = new ClaimsIdentity(claims, "jwt");
        var user = new ClaimsPrincipal(identity);
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        AuthenticationStateChangedEvent?.Invoke();
    }

    public void NotifyUserLogout()
    {
        var anonymous = new ClaimsPrincipal(new ClaimsIdentity());
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(anonymous)));
        AuthenticationStateChangedEvent?.Invoke();
    }

    private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
    {
        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(jwt);
        var claims = token.Claims.ToList();

        // Rollen toevoegen op basis van "role" claim (JWT kan single of array bevatten)
        var roleClaims = token.Claims.Where(c => c.Type == "role");
        foreach (var roleClaim in roleClaims)
        {
            claims.Add(new Claim(ClaimTypes.Role, roleClaim.Value));
        }

        return claims;
    }
    private bool IsTokenExpired(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);

        var expClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == "exp");
        if (expClaim == null) return true;

        var expDate = DateTimeOffset.FromUnixTimeSeconds(long.Parse(expClaim.Value));
        return expDate < DateTimeOffset.UtcNow;
    }
}
