using Microsoft.EntityFrameworkCore;
using RSW.Infrastructure.Data;
using RSW.Domain.Entities;
using RSW.Application.Interfaces;

namespace RSW.API.Endpoints
{
    public static class JurySlotEndpoints
    {
        public static void MapJurySlotEndpoints(this WebApplication app)
        {
            var JurySlot = app.MapGroup("/api/JurySlot").WithTags("JurySlot");

            JurySlot.MapGet("/", async (IJurySlotService service) =>
            {
                return Results.Ok(await service.GetAllAsync());
            }).AllowAnonymous();

            JurySlot.MapGet("/{id:guid}", async (Guid id, IJurySlotService service) =>
            {
                var found = await service.GetByIdAsync(id);
                return found is not null ? Results.Ok(found) : Results.NotFound();
            }).AllowAnonymous();

            JurySlot.MapPost("/", async (JurySlot model, IJurySlotService service) =>
            {
                var created = await service.CreateAsync(model);
                return Results.Created($"/api/JurySlot/{created.Id}", created);
            }).RequireAuthorization(policy => policy.RequireRole("Admin"));

            JurySlot.MapPut("/{id:guid}", async (Guid id, JurySlot model, IJurySlotService service) =>
            {
                var updated = await service.UpdateAsync(id, model);
                return updated is not null ? Results.Ok(updated) : Results.NotFound();
            }).RequireAuthorization(policy => policy.RequireRole("Admin"));

            JurySlot.MapDelete("/{id:guid}", async (Guid id, IJurySlotService service) =>
            {
                var success = await service.DeleteAsync(id);
                return success ? Results.Ok() : Results.NotFound();
            }).RequireAuthorization(policy => policy.RequireRole("Admin"));
        }
    }
}
