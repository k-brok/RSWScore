using Microsoft.EntityFrameworkCore;
using RSW.API.Data;
using RSW.Shared.Entities;
using RSW.Shared.Interfaces;

namespace RSW.API.Endpoints
{
    public static class SignupCodeEndpoints
    {
        public static void MapSignupCodeEndpoints(this WebApplication app)
        {
            var SignupCode = app.MapGroup("/api/SignupCode").WithTags("SignupCode");

            SignupCode.MapGet("/", async (ISignupCodeService service) =>
            {
                return Results.Ok(await service.GetAllAsync());
            }).AllowAnonymous();

            SignupCode.MapGet("/{id:guid}", async (Guid id, ISignupCodeService service) =>
            {
                var found = await service.GetByIdAsync(id);
                return found is not null ? Results.Ok(found) : Results.NotFound();
            }).AllowAnonymous();

            SignupCode.MapPost("/", async (SignupCode model, ISignupCodeService service) =>
            {
                var created = await service.CreateAsync(model);
                return Results.Created($"/api/SignupCode/{created.Id}", created);
            });

            SignupCode.MapPut("/{id:guid}", async (Guid id, SignupCode model, ISignupCodeService service) =>
            {
                var updated = await service.UpdateAsync(id, model);
                return updated is not null ? Results.Ok(updated) : Results.NotFound();
            });

            SignupCode.MapDelete("/{id:guid}", async (Guid id, ISignupCodeService service) =>
            {
                var success = await service.DeleteAsync(id);
                return success ? Results.Ok() : Results.NotFound();
            });
            SignupCode.MapGet("/validate/{id:guid}", async (Guid id, ISignupCodeService service) =>
            {
                var found = await service.ValidateAsync(id);
                return found is not null ? Results.Ok(found) : Results.NotFound();
            }).AllowAnonymous();
        }
    }
}
