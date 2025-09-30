using RSW.Shared.Dto;
using RSW.Shared.Interfaces;

namespace RSW.API.Endpoints;

public static class EndpointRouteBuilderExtensions
{
    public static void MapAccountEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/account").WithTags("Account");

        // ----------------- LOGIN -----------------
        group.MapPost("/login", async (LoginRequest request, IAuthService authService) =>
        {
            var response = await authService.LoginAsync(request, rememberMe: false);
            if (response == null)
                return Results.Unauthorized();

            return Results.Ok(response);
        }).AllowAnonymous();

        // ----------------- REGISTER -----------------
        group.MapPost("/register", async (RegisterRequest request, IAuthService authService) =>
        {
            var response = await authService.RegisterAsync(request, rememberMe: false);
            if (response == null)
                return Results.BadRequest(new { Message = "Registration failed" });

            return Results.Ok(response);
        }).AllowAnonymous();

        // ----------------- REQUEST PASSWORD RESET -----------------
        group.MapPost("/generate-reset-token", async (ResetPasswordTokenRequest request, IAuthService authService) =>
        {
            await authService.GeneratePasswordResetTokenAsync(request);
            return Results.Ok(new { Message = "Password reset token sent (if user exists)" });
        }).AllowAnonymous();

        // ----------------- RESET PASSWORD -----------------
        group.MapPost("/reset-password", async (ResetPasswordRequest request, IAuthService authService) =>
        {
            var success = await authService.ResetPasswordAsync(request);
            return success
                ? Results.Ok(new { Message = "Password changed successfully" })
                : Results.BadRequest(new { Message = "Invalid request or token" });
        }).AllowAnonymous();

        // ----------------- GET CURRENT USER -----------------
        group.MapGet("/getcurrentuser", async (IAuthService authService) =>
        {
            var user = await authService.GetCurrentUserAsync();
            if (user == null) return Results.Unauthorized();

            // Tip: eventueel alleen relevante properties terugsturen i.p.v. hele ApplicationUser
            return Results.Ok(user);
        }).RequireAuthorization();
        
        group.MapGet("/confirmemail", async (
            [AsParameters] ConfirmEmailRequest request,
            IAuthService authService) =>
        {
            var response = await authService.ConfirmEmailAsync(request);

            if (!response.Success)
                return Results.BadRequest(response);

            return Results.Ok(response);
        });
    }
}
