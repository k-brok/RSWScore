using Microsoft.EntityFrameworkCore;
using RSW.API.Data;
using RSW.Shared.Dto;
using RSW.Shared.Entities;
using RSW.Shared.Interfaces;

namespace RSW.API.Endpoints
{
    public static class ScoutEndpoints
    {
        public static void MapScoutEndpoints(this WebApplication app)
        {
            var Scout = app.MapGroup("/api/Scout").WithTags("Scout");

            Scout.MapGet("/", async (IScoutService service) =>
            {
                return Results.Ok(await service.GetAllAsync());
            }).AllowAnonymous();

            Scout.MapGet("/{id:guid}", async (Guid id, IScoutService service) =>
            {
                var found = await service.GetByIdAsync(id);
                return found is not null ? Results.Ok(found) : Results.NotFound();
            }).AllowAnonymous();

            Scout.MapPost("/", async (ScoutCreateDto dto, IScoutService service) =>
            {
                var created = await service.CreateAsync(dto);
                return Results.Created($"/api/Scout/{created.Id}", created);
            });

            Scout.MapPut("/{id:guid}", async (Guid id, ScoutDto dto, IScoutService service) =>
            {
                var updated = await service.UpdateAsync(id, dto);
                return updated is not null ? Results.Ok(updated) : Results.NotFound();
            });

            Scout.MapDelete("/{id:guid}", async (Guid id, IScoutService service) =>
            {
                var success = await service.DeleteAsync(id);
                return success ? Results.Ok() : Results.NotFound();
            });
        }
    }
}
