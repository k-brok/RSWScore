using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Chunkk.JWT.Client;

public class AuthService
{
    private readonly HttpClient _http;
    private readonly ITokenStorage _tokenStorage;

    public AuthService(HttpClient http, ITokenStorage tokenStorage)
    {
        _http = http;
        _tokenStorage = tokenStorage;
    }

    public async Task<LoginResponse?> LoginAsync(LoginRequest request, bool rememberMe)
    {
        var response = await _http.PostAsJsonAsync("/account/login", request);

        if (!response.IsSuccessStatusCode)
            return null;

        var result = await response.Content.ReadFromJsonAsync<LoginResponse>();
        if (result != null && !string.IsNullOrEmpty(result.Token))
        {
            await _tokenStorage.SetTokenAsync(result.Token,rememberMe);
        }
        return result;
    }

    public async Task LogoutAsync()
    {
        await _tokenStorage.RemoveTokenAsync();
    }

    // Voorbeeld: current user ophalen
    public async Task<UserInfo?> GetCurrentUser()
    {
        return await _http.GetFromJsonAsync<UserInfo>("/account/me");
    }
}

public record LoginRequest(string Email, string Password);
public record LoginResponse(string Token, string? Message);
public record UserInfo(string Username);
