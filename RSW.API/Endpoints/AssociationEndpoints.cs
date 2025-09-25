using Microsoft.EntityFrameworkCore;
using RSW.API.Data;
using RSW.Shared.Entities;
using RSW.Shared.Interfaces;

namespace RSW.API.Endpoints
{
    public static class AssociationEndpoints
    {
        public static void MapAssociationEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/association").WithTags("Association");

            group.MapGet("/", async (IAssociationService service) =>
            {
                return Results.Ok(await service.GetAllAsync());
            }).AllowAnonymous();

            group.MapGet("/{id:guid}", async (Guid id, IAssociationService service) =>
            {
                var found = await service.GetByIdAsync(id);
                return found is not null ? Results.Ok(found) : Results.NotFound();
            }).AllowAnonymous();

            group.MapPost("/", async (Association model, IAssociationService service) =>
            {
                var created = await service.CreateAsync(model);
                return Results.Created($"/api/Association/{created.Id}", created);
            });

            group.MapPut("/{id:guid}", async (Guid id, Association model, IAssociationService service) =>
            {
                var updated = await service.UpdateAsync(id, model);
                return updated is not null ? Results.Ok(updated) : Results.NotFound();
            });

            group.MapDelete("/{id:guid}", async (Guid id, IAssociationService service) =>
            {
                var success = await service.DeleteAsync(id);
                return success ? Results.Ok() : Results.NotFound();
            });

            group.MapGet("/{id:guid}/units", async (Guid id, IAssociationService service) =>
            {
                return Results.Ok(await service.GetUnitsAsync(id));
            }).AllowAnonymous();
        }
    }
}
