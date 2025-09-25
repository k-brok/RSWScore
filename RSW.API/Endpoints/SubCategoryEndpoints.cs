using Microsoft.EntityFrameworkCore;
using RSW.API.Data;
using RSW.Shared.Entities;
using RSW.Shared.Interfaces;

namespace RSW.API.Endpoints
{
    public static class SubCategoryEndpoints
    {
        public static void MapSubCategoryEndpoints(this WebApplication app)
        {
            var SubCategory = app.MapGroup("/api/SubCategory").WithTags("SubCategory");

            SubCategory.MapGet("/", async (ISubCategoryService service) =>
            {
                return Results.Ok(await service.GetAllAsync());
            }).AllowAnonymous();

            SubCategory.MapGet("/{id:guid}", async (Guid id, ISubCategoryService service) =>
            {
                var found = await service.GetByIdAsync(id);
                return found is not null ? Results.Ok(found) : Results.NotFound();
            }).AllowAnonymous();

            SubCategory.MapPost("/", async (SubCategory model, ISubCategoryService service) =>
            {
                var created = await service.CreateAsync(model);
                return Results.Created($"/api/SubCategory/{created.Id}", created);
            });

            SubCategory.MapPut("/{id:guid}", async (Guid id, SubCategory model, ISubCategoryService service) =>
            {
                var updated = await service.UpdateAsync(id, model);
                return updated is not null ? Results.Ok(updated) : Results.NotFound();
            });

            SubCategory.MapDelete("/{id:guid}", async (Guid id, ISubCategoryService service) =>
            {
                var success = await service.DeleteAsync(id);
                return success ? Results.Ok() : Results.NotFound();
            });
            SubCategory.MapGet("/{id:guid}/criterias", async (Guid id, ISubCategoryService service) =>
            {
                var found = await service.GetCriteriasAsync(id);
                return found is not null ? Results.Ok(found) : Results.NotFound();
            }).AllowAnonymous();
        }
    }
}
