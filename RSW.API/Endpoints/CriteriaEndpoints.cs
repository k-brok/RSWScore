using Microsoft.EntityFrameworkCore;
using RSW.API.Data;
using RSW.Shared.Entities;
using RSW.Shared.Interfaces;

namespace RSW.API.Endpoints
{
    public static class CriteriaEndpoints
    {
        public static void MapCriteriaEndpoints(this WebApplication app)
        {
            var Criteria = app.MapGroup("/api/Criteria").WithTags("Criteria");

            Criteria.MapGet("/", async (ICriteriaService service) =>
            {
                return Results.Ok(await service.GetAllAsync());
            }).AllowAnonymous();

            Criteria.MapGet("/{id:guid}", async (Guid id, ICriteriaService service) =>
            {
                var found = await service.GetByIdAsync(id);
                return found is not null ? Results.Ok(found) : Results.NotFound();
            }).AllowAnonymous();

            Criteria.MapPost("/", async (Criteria model, ICriteriaService service) =>
            {
                var created = await service.CreateAsync(model);
                return Results.Created($"/api/Criteria/{created.Id}", created);
            });

            Criteria.MapPut("/{id:guid}", async (Guid id, Criteria model, ICriteriaService service) =>
            {
                var updated = await service.UpdateAsync(id, model);
                return updated is not null ? Results.Ok(updated) : Results.NotFound();
            });

            Criteria.MapDelete("/{id:guid}", async (Guid id, ICriteriaService service) =>
            {
                var success = await service.DeleteAsync(id);
                return success ? Results.Ok() : Results.NotFound();
            });
        }
    }
}
