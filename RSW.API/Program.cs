using Microsoft.EntityFrameworkCore;
using RSW.API.Data;
using RSW.API.Endpoints;
using RSW.API.Services;
using RSW.Shared.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using RSW.Shared.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using System.Security.Claims;

namespace RSW.API;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<AppDbContext>(options =>
            options
                .UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"))
                .LogTo(Console.WriteLine, LogLevel.Information)
        );

        builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();

        builder.Services.AddScoped<IAssociationService, AssociationService>();
        builder.Services.AddScoped<ICategoryService, CategoryService>();
        builder.Services.AddScoped<ICriteriaService, CriteriaService>();
        builder.Services.AddScoped<IEditionService, EditionService>();
        builder.Services.AddScoped<IUnitService, UnitService>();
        builder.Services.AddScoped<IJurySlotService, JurySlotService>();
        builder.Services.AddScoped<IPatrolService, PatrolService>();
        builder.Services.AddScoped<IScoreService, ScoreService>();
        builder.Services.AddScoped<IScoutService, ScoutService>();
        builder.Services.AddScoped<ISignupCodeService, SignupCodeService>();
        builder.Services.AddScoped<ISubCategoryService, SubCategoryService>();
        builder.Services.AddScoped<ISubGroupService, SubGroupService>();
        builder.Services.AddScoped<IWebSettingService, WebSettingService>();
        builder.Services.AddScoped<IVolunteerTaskService, VolunteerTaskService>();
        builder.Services.AddScoped<IVolunteerAssignmentService, VolunteerAssignmentService>();
        builder.Services.AddScoped<IUserService, UserService>();

        builder.Services.AddScoped<SeedService>();

        var jwtSection = builder.Configuration.GetSection("Jwt");
        var key = jwtSection["Key"] ?? throw new Exception("Jwt:Key is missing");
        var issuer = jwtSection["Issuer"];
        var audience = jwtSection["Audience"];

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = "Bearer";
            options.DefaultChallengeScheme = "Bearer";
        })
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
                RoleClaimType = ClaimTypes.Role,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
            };
        });

        builder.Services.AddAuthorization();

        builder.Services.AddScoped<IAuthService, AuthService>();
        builder.Services.AddScoped<IJwtService, JwtService>();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new() { Title = "RSW API", Version = "v1" });

                c.AddSecurityDefinition("Bearer", new()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Voer hier je JWT in. Voorbeeld: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

        builder.Services.AddSignalR();

        var appBaseUrl = builder.Configuration["AppBaseUrl"];
        builder.Services.AddCors(options =>
        {
            options.AddPolicy("AllowBlazorClient",
                policy => policy.WithOrigins(appBaseUrl!)
                                .AllowAnyHeader()
                                .AllowAnyMethod());
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        //app.UseHttpsRedirection();

        app.UseCors("AllowBlazorClient");
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapAssociationEndpoints();
        app.MapCategoryEndpoints();
        app.MapCriteriaEndpoints();
        app.MapEditionEndpoints();
        app.MapUnitEndpoints();
        app.MapJurySlotEndpoints();
        app.MapPatrolEndpoints();
        app.MapScoreEndpoints();
        app.MapScoutEndpoints();
        app.MapSignupCodeEndpoints();
        app.MapSubCategoryEndpoints();
        app.MapSubGroupEndpoints();
        app.MapWebSettingEndpoints();
        app.MapVolunteerTaskEndpoints();
        app.MapVolunteerAssignmentEndpoints();
        app.MapUserEndpoints();
        app.MapAccountEndpoints();


        using (var scope = app.Services.CreateScope())
        {
            var seedService = scope.ServiceProvider.GetRequiredService<SeedService>();

            var adminEmail = builder.Configuration["AdminUser:Email"] ?? "admin@example.com";
            var adminPassword = builder.Configuration["AdminUser:Password"] ?? "Admin123!";

            await seedService.SeedAsync(adminEmail, adminPassword);
        }
        app.MapHub<UpdatesHub>("/hubs/updates");

        app.Run();
    }
}
