using Microsoft.JSInterop;
using RSW.Shared.Interfaces;

namespace RSW.APP.Services;

public class BrowserTokenStorage : ITokenStorage
{
    private readonly IJSRuntime _js;
    private const string Key = "jwt_token";

    public BrowserTokenStorage(IJSRuntime js)
    {
        _js = js;
    }

    public async Task SetTokenAsync(string token, bool rememberMe)
    {
        if (rememberMe)
            await _js.InvokeVoidAsync("localStorage.setItem", Key, token);
        else
            await _js.InvokeVoidAsync("sessionStorage.setItem", Key, token);
    }

    public async Task<string?> GetTokenAsync()
    {
        var token = await _js.InvokeAsync<string>("localStorage.getItem", Key);
        if (!string.IsNullOrEmpty(token))
            return token;

        token = await _js.InvokeAsync<string>("sessionStorage.getItem", Key);
        return token;
    }

    public async Task RemoveTokenAsync()
    {
        await _js.InvokeVoidAsync("localStorage.removeItem", Key);
        await _js.InvokeVoidAsync("sessionStorage.removeItem", Key);
    }
}
