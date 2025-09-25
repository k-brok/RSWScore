using Microsoft.EntityFrameworkCore;
using RSW.API.Data;
using RSW.Shared.Entities;
using RSW.Shared.Interfaces;

namespace RSW.API.Endpoints
{
    public static class UnitEndpoints
    {
        public static void MapUnitEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/Unit").WithTags("Unit");

            group.MapGet("/", async (IUnitService service) =>
            {
                return Results.Ok(await service.GetAllAsync());
            }).AllowAnonymous();

            group.MapGet("/{id:guid}", async (Guid id, IUnitService service) =>
            {
                var found = await service.GetByIdAsync(id);
                return found is not null ? Results.Ok(found) : Results.NotFound();
            }).AllowAnonymous();

            group.MapPost("/", async (Unit model, IUnitService service) =>
            {
                var created = await service.CreateAsync(model);
                return Results.Created($"/api/Unit/{created.Id}", created);
            });

            group.MapPut("/{id:guid}", async (Guid id, Unit model, IUnitService service) =>
            {
                var updated = await service.UpdateAsync(id, model);
                return updated is not null ? Results.Ok(updated) : Results.NotFound();
            });

            group.MapDelete("/{id:guid}", async (Guid id, IUnitService service) =>
            {
                var success = await service.DeleteAsync(id);
                return success ? Results.Ok() : Results.NotFound();
            });

            group.MapGet("/{id:guid}/patrols", async (Guid id, IUnitService service) =>
            {
                return Results.Ok(await service.GetPatrolsAsync(id));
            }).AllowAnonymous();
        }
    }
}
