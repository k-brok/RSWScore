using RSW.API.Services;
using RSW.Shared.Dto;

namespace RSW.API.Endpoints;

public static class EndpointRouteBuilderExtensions
{
    public static void MapAccountEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/account").WithTags("Account");

        // ----------------- LOGIN -----------------
        group.MapPost("/login", async (LoginRequest request, IAuthService authService) =>
        {
            var token = await authService.LoginAsync(request.Email, request.Password);
            if (token == null)
                return Results.Unauthorized();

            return Results.Ok(new LoginResponse("Login successful", true, token));
        }).AllowAnonymous();

        // ----------------- REGISTER -----------------
        group.MapPost("/register", async (RegisterRequest request, IAuthService authService) =>
        {
            var token = await authService.RegisterAsync(request.Email, request.Password);
            if (token == null)
                return Results.BadRequest("Registration failed");

            return Results.Ok(new RegisterResponse("User registered successfully", true, token));
        }).AllowAnonymous();

        // ----------------- REQUEST PASSWORD RESET -----------------
        group.MapPost("/request-password-change", async (ResetPasswordTokenRequest request, IAuthService authService) =>
        {
            await authService.GeneratePasswordResetTokenAsync(request.Email);

            return Results.Ok();
        }).AllowAnonymous();

        // ----------------- RESET PASSWORD -----------------
        group.MapPost("/password-change", async (ResetPasswordRequest request, IAuthService authService) =>
        {
            var success = await authService.ResetPasswordAsync(request.Email, request.Token, request.NewPassword);
            return success
                ? Results.Ok(new { Message = "Password changed successfully" })
                : Results.BadRequest("Invalid request or token");
        }).AllowAnonymous();

        group.MapGet("/getcurrentuser", async (IAuthService authService) =>
        {
            Console.WriteLine("hallo?");
            var user = await authService.GetCurrentUserAsync();
            if (user == null) return Results.Unauthorized();

            // Return enkel relevante info
            return Results.Ok(user);
        }).RequireAuthorization();
    }
}
