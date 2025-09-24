using Microsoft.EntityFrameworkCore;
using RSW.API.Data;
using RSW.Shared.Dto;
using RSW.Shared.Entities;
using RSW.Shared.Interfaces;

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

            JurySlot.MapPost("/", async (JurySlotCreateDto dto, IJurySlotService service) =>
            {
                var created = await service.CreateAsync(dto);
                return Results.Created($"/api/JurySlot/{created.Id}", created);
            }).RequireAuthorization(policy => policy.RequireRole("Admin"));

            JurySlot.MapPut("/{id:guid}", async (Guid id, JurySlotDto dto, IJurySlotService service) =>
            {
                var updated = await service.UpdateAsync(id, dto);
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
