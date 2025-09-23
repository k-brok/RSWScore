using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Chunkk.JWT.Server.Services;
using Microsoft.AspNetCore.Http;

namespace Chunkk.JWT.Server;

public static class EndpointRouteBuilderExtensions
{
    public static IEndpointRouteBuilder MapAccountEndpoints<TUser>(this IEndpointRouteBuilder endpoints) 
        where TUser : class
    {
        var group = endpoints.MapGroup("/account").WithTags("Account");

        // ----------------- LOGIN -----------------
        group.MapPost("/login", async (LoginRequest request, IAuthService<TUser> authService) =>
        {
            var token = await authService.LoginAsync(request.Email, request.Password);
            if (token == null)
                return Results.Unauthorized();

            return Results.Ok(new LoginResponse("Login successful", true, token));
        }).AllowAnonymous();

        // ----------------- REGISTER -----------------
        group.MapPost("/register", async (RegisterRequest request, IAuthService<TUser> authService) =>
        {
            var token = await authService.RegisterAsync(request.Email, request.Password);
            if (token == null)
                return Results.BadRequest("Registration failed");

            return Results.Ok(new RegisterResponse("User registered successfully", true, token));
        }).AllowAnonymous();

        // ----------------- REQUEST PASSWORD RESET -----------------
        group.MapPost("/request-password-change", async (ResetPasswordTokenRequest request, IAuthService<TUser> authService) =>
        {
            var token = await authService.GeneratePasswordResetTokenAsync(request.Email);
            if (token == null)
                return Results.BadRequest("User not found");

            // Hier zou je een email trigger kunnen toevoegen in de toekomst
            return Results.Ok(new { Message = "Reset token generated, check email for instructions", Token = token });
        }).AllowAnonymous();

        // ----------------- RESET PASSWORD -----------------
        group.MapPost("/password-change", async (ResetPasswordRequest request, IAuthService<TUser> authService) =>
        {
            var success = await authService.ResetPasswordAsync(request.Email, request.Token, request.NewPassword);
            return success 
                ? Results.Ok(new { Message = "Password changed successfully" }) 
                : Results.BadRequest("Invalid request or token");
        }).AllowAnonymous();

        return endpoints;
    }
}

// ----------------- DTO’s -----------------
public record LoginRequest(string Email, string Password);

public record RegisterRequest(string Email, string Password);

public record ResetPasswordTokenRequest(string Email);

public record ResetPasswordRequest(string Email, string Token, string NewPassword);

public record LoginResponse(string Message, bool Success, string Token);

public record RegisterResponse(string Message, bool Success, string Token);
