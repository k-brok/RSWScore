using Microsoft.EntityFrameworkCore;
using RSW.Infrastructure.Data;
using RSW.Domain.Entities;
using RSW.Application.Interfaces;

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

            Patrol.MapGet("/{id:guid}/scouts", async (Guid id, IPatrolService service) =>
            {
                return Results.Ok(await service.GetScoutsAsync(id));
            }).AllowAnonymous();

            Patrol.MapGet("/{id:guid}/scores", async (Guid id, IPatrolService service) =>
            {
                return Results.Ok(await service.GetScoresAsync(id));
            }).AllowAnonymous();

            Patrol.MapPost("/", async (Patrol model, IPatrolService service) =>
            {
                var created = await service.CreateAsync(model);
                return Results.Created($"/api/Patrol/{created.Id}", created);
            });

            Patrol.MapPut("/{id:guid}", async (Guid id, Patrol model, IPatrolService service) =>
            {
                var updated = await service.UpdateAsync(id, model);
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
