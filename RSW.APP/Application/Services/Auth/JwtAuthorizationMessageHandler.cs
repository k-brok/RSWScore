using System.Net.Http.Headers;
using RSW.APP.Application.Services;
using RSW.Application.Interfaces;

namespace RSW.APP.Application.Services;

public class JwtAuthorizationMessageHandler : DelegatingHandler
{
    private readonly ITokenStorage _localStorage;

    public JwtAuthorizationMessageHandler(ITokenStorage localStorage)
    {
        _localStorage = localStorage;

        // stel een standaard inner handler in
        InnerHandler = new HttpClientHandler();
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var token = await _localStorage.GetTokenAsync();
        if (!string.IsNullOrWhiteSpace(token))
        {
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        return await base.SendAsync(request, cancellationToken);
    }
}
