using Microsoft.EntityFrameworkCore;
using RSW.Infrastructure.Data;
using RSW.Domain.Entities;
using RSW.Application.Interfaces;

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

            Scout.MapPost("/", async (Scout model, IScoutService service) =>
            {
                var created = await service.CreateAsync(model);
                return Results.Created($"/api/Scout/{created.Id}", created);
            });

            Scout.MapPut("/{id:guid}", async (Guid id, Scout model, IScoutService service) =>
            {
                var updated = await service.UpdateAsync(id, model);
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
