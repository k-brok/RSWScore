using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;

namespace Chunkk.JWT.Client;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddChunkkJwt(this IServiceCollection services, Uri apiBaseAddress)
    {
        services.AddScoped<ITokenStorage, BrowserTokenStorage>();
        services.AddScoped<AuthService>();
        services.AddScoped<AuthenticationStateProvider, JwtAuthenticationStateProvider>();
        services.AddScoped<JwtAuthenticationStateProvider>();

        services.AddScoped<JwtAuthorizationMessageHandler>();

        services.AddScoped(sp =>
        {
            var handler = sp.GetRequiredService<JwtAuthorizationMessageHandler>();
            return new HttpClient(handler)
            {
                BaseAddress = apiBaseAddress
            };
        });

        return services;
    }
}
