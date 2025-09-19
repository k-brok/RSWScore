using Microsoft.EntityFrameworkCore;
using RSW.API.Data;
using RSW.Shared.Dto;
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

            SignupCode.MapPost("/", async (SignupCodeCreateDto dto, ISignupCodeService service) =>
            {
                var created = await service.CreateAsync(dto);
                return Results.Created($"/api/SignupCode/{created.Id}", created);
            });

            SignupCode.MapPut("/{id:guid}", async (Guid id, SignupCodeUpdateDto dto, ISignupCodeService service) =>
            {
                var updated = await service.UpdateAsync(id, dto);
                return updated is not null ? Results.Ok(updated) : Results.NotFound();
            });

            SignupCode.MapDelete("/{id:guid}", async (Guid id, ISignupCodeService service) =>
            {
                var success = await service.DeleteAsync(id);
                return success ? Results.Ok() : Results.NotFound();
            });
        }
    }
}
