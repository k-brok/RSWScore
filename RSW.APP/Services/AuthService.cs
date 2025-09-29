using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using RSW.Shared.Entities;
using RSW.Shared.Dto;
using RSW.Shared.Interfaces;

namespace RSW.APP.Services;

public class AuthService
{
    private readonly HttpClient _http;
    private readonly ITokenStorage _tokenStorage;
    public ApplicationUser CurrentUser { get; set; }
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
        CurrentUser = await GetCurrentUser();
    }

    return result;
}

    public async Task LogoutAsync()
    {
        await _tokenStorage.RemoveTokenAsync();
        CurrentUser = null;

    }

    // Voorbeeld: current user ophalen
    public async Task<ApplicationUser?> GetCurrentUser()
    {
        try
        {
            var user = await _http.GetFromJsonAsync<ApplicationUser>($"{Endpoint}/getcurrentuser");
            CurrentUser = user;
            return user;
        }
        catch
        {
            CurrentUser = null;
            return null;
        }
    }
    public async Task<RegisterResponse?> RegisterAsync(string email, string password, bool rememberMe)
    {
        var request = new RegisterRequest(email, password);
        var response = await _http.PostAsJsonAsync($"{Endpoint}/register", request);

        if (!response.IsSuccessStatusCode)
            return null;

        var result = await response.Content.ReadFromJsonAsync<RegisterResponse>();
        if (result != null && !string.IsNullOrEmpty(result.Token))
        {
            await _tokenStorage.SetTokenAsync(result.Token, rememberMe);
            CurrentUser = await GetCurrentUser();
        }

        return result;
    }
}

