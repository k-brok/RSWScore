using Microsoft.EntityFrameworkCore;
using RSW.API.Data;
using RSW.Shared.Entities;
using RSW.Shared.Interfaces;

namespace RSW.API.Endpoints
{
    public static class SubGroupEndpoints
    {
        public static void MapSubGroupEndpoints(this WebApplication app)
        {
            var SubGroup = app.MapGroup("/api/SubGroup").WithTags("SubGroup");

            SubGroup.MapGet("/", async (ISubGroupService service) =>
            {
                return Results.Ok(await service.GetAllAsync());
            }).AllowAnonymous();

            SubGroup.MapGet("/{id:guid}", async (Guid id, ISubGroupService service) =>
            {
                var found = await service.GetByIdAsync(id);
                return found is not null ? Results.Ok(found) : Results.NotFound();
            }).AllowAnonymous();

            SubGroup.MapPost("/", async (SubGroup model, ISubGroupService service) =>
            {
                var created = await service.CreateAsync(model);
                return Results.Created($"/api/SubGroup/{created.Id}", created);
            });

            SubGroup.MapPut("/{id:guid}", async (Guid id, SubGroup model, ISubGroupService service) =>
            {
                var updated = await service.UpdateAsync(id, model);
                return updated is not null ? Results.Ok(updated) : Results.NotFound();
            });

            SubGroup.MapDelete("/{id:guid}", async (Guid id, ISubGroupService service) =>
            {
                var success = await service.DeleteAsync(id);
                return success ? Results.Ok() : Results.NotFound();
            });
        }
    }
}
