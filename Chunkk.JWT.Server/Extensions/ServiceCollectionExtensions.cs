using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Chunkk.JWT.Server.Services;

namespace Chunkk.JWT.Server;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddChunkkJwt<TUser>(this IServiceCollection services, IConfiguration config)
        where TUser : class
    {

        var jwtSection = config.GetSection("Jwt");
        var key = jwtSection["Key"] ?? throw new Exception("Jwt:Key is missing");
        var issuer = jwtSection["Issuer"];
        var audience = jwtSection["Audience"];

        services.AddAuthentication("Bearer")
            .AddJwtBearer("Bearer", options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
                };
            });

        services.AddAuthorization();

        services.AddScoped<IAuthService<TUser>, AuthService<TUser>>();
        services.AddScoped<IJwtService, JwtService>();

        services.AddCors(options =>
        {
            options.AddPolicy("AllowBlazorClient",
                policy => policy.WithOrigins(jwtSection["Audience"]!)
                                .AllowAnyHeader()
                                .AllowAnyMethod());
        });

        return services;
    }
}
