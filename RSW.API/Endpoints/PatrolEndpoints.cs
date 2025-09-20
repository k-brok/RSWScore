using Microsoft.EntityFrameworkCore;
using RSW.API.Data;
using RSW.Shared.Dto;
using RSW.Shared.Entities;
using RSW.Shared.Interfaces;

namespace RSW.API.Endpoints
{
    public static class PatrolEndpoints
    {
        public static void MapPatrolEndpoints(this WebApplication app)
        {
            var Patrol = app.MapGroup("/api/Patrol").WithTags("Patrol");

            Patrol.MapGet("/", async (IPatrolService service) =>
            {
                return Results.Ok(await service.GetAllAsync());
            }).AllowAnonymous();

            Patrol.MapGet("/{id:guid}", async (Guid id, IPatrolService service) =>
            {
                var found = await service.GetByIdAsync(id);
                return found is not null ? Results.Ok(found) : Results.NotFound();
            }).AllowAnonymous();

            Patrol.MapPost("/", async (PatrolCreateDto dto, IPatrolService service) =>
            {
                var created = await service.CreateAsync(dto);
                return Results.Created($"/api/Patrol/{created.Id}", created);
            });

            Patrol.MapPut("/{id:guid}", async (Guid id, PatrolDto dto, IPatrolService service) =>
            {
                var updated = await service.UpdateAsync(id, dto);
                return updated is not null ? Results.Ok(updated) : Results.NotFound();
            });

            Patrol.MapDelete("/{id:guid}", async (Guid id, IPatrolService service) =>
            {
                var success = await service.DeleteAsync(id);
                return success ? Results.Ok() : Results.NotFound();
            });
        }
    }
}
