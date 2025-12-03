using Microsoft.EntityFrameworkCore;
using RSW.Infrastructure.Data;
using RSW.Domain.Entities;
using RSW.Application.Interfaces;

namespace RSW.API.Endpoints
{
    public static class CategoryEndpoints
    {
        public static void MapCategoryEndpoints(this WebApplication app)
        {
            var Category = app.MapGroup("/api/Category").WithTags("Category");

            Category.MapGet("/", async (ICategoryService service) =>
            {
                return Results.Ok(await service.GetAllAsync());
            }).AllowAnonymous();

            Category.MapGet("/{id:guid}", async (Guid id, ICategoryService service) =>
            {
                var found = await service.GetByIdAsync(id);
                return found is not null ? Results.Ok(found) : Results.NotFound();
            }).AllowAnonymous();

            Category.MapPost("/", async (Category model, ICategoryService service) =>
            {
                var created = await service.CreateAsync(model);
                return Results.Created($"/api/Category/{created.Id}", created);
            });

            Category.MapPut("/{id:guid}", async (Guid id, Category model, ICategoryService service) =>
            {
                var updated = await service.UpdateAsync(id, model);
                return updated is not null ? Results.Ok(updated) : Results.NotFound();
            });

            Category.MapDelete("/{id:guid}", async (Guid id, ICategoryService service) =>
            {
                var success = await service.DeleteAsync(id);
                return success ? Results.Ok() : Results.NotFound();
            });

            Category.MapGet("/{id:guid}/subcategories", async (Guid id, ICategoryService service) =>
            {
                return Results.Ok(await service.GetSubCategorysAsync(id));
            }).AllowAnonymous();
        }
    }
}
