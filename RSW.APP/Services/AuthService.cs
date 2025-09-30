using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using RSW.Shared.Entities;
using RSW.Shared.Dto;
using RSW.Shared.Interfaces;
using System.Security.Claims;

namespace RSW.APP.Services;

public class AuthService : IAuthService
{
    private readonly HttpClient _http;
    private readonly ITokenStorage _tokenStorage;
    private const string Endpoint = "api/account";

    public AuthService(HttpClient http, ITokenStorage tokenStorage)
    {
        _http = http;
        _tokenStorage = tokenStorage;
    }

    public async Task<LoginResponse?> LoginAsync(LoginRequest request, bool rememberMe)
    {
        var response = await _http.PostAsJsonAsync($"{Endpoint}/login", request);

        if (!response.IsSuccessStatusCode)
            return null;

        var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
        if (result != null && !string.IsNullOrEmpty(result.Token))
        {
            await _tokenStorage.SetTokenAsync(result.Token, rememberMe);
            await GetCurrentUserAsync();
        }

        return result;
    }

    public async Task LogoutAsync()
    {
        await _tokenStorage.RemoveTokenAsync();

    }
    public async Task<ApplicationUser?> GetCurrentUserAsync()
    {
        var result = await _http.GetAsync($"{Endpoint}/getcurrentuser");
        if (result.IsSuccessStatusCode)
        {
            return await result.Content.ReadFromJsonAsync<ApplicationUser>();
        }

        return null;
    }
    public async Task<RegisterResponse?> RegisterAsync(RegisterRequest request, bool rememberMe)
    {
        var response = await _http.PostAsJsonAsync($"{Endpoint}/register", request);

        if (!response.IsSuccessStatusCode)
            return null;

        var result = await response.Content.ReadFromJsonAsync<RegisterResponse>();
        if (result != null && !string.IsNullOrEmpty(result.Token))
        {
            await _tokenStorage.SetTokenAsync(result.Token, rememberMe);
            await GetCurrentUserAsync();
        }

        return result;
    }

    public async Task GeneratePasswordResetTokenAsync(ResetPasswordTokenRequest request)
    {
        var response = await _http.PostAsJsonAsync($"{Endpoint}/generate-reset-token", request);

        if (!response.IsSuccessStatusCode)
        {
            throw new HttpRequestException("Kan reset token niet genereren");
        }
    }

    public async Task<bool> ResetPasswordAsync(ResetPasswordRequest request)
    {
        var response = await _http.PostAsJsonAsync($"{Endpoint}/reset-password", request);

        if (!response.IsSuccessStatusCode)
            return false;

        return true;
    }

    public async Task<IList<Claim>> GetClaimsForUserAsync(ApplicationUser user)
    {
        var claims = new List<Claim>();

        if (user == null)
            return claims;

        if (!string.IsNullOrEmpty(user.Email))
            claims.Add(new Claim(ClaimTypes.Email, user.Email));

        if (!string.IsNullOrEmpty(user.Id))
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));

        if (!string.IsNullOrEmpty(user.UserName))
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));

        return claims;
    }
}

