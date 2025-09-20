using RSW.API.Services;
using RSW.Shared.Dto;
using RSW.Shared.Interfaces;

namespace RSW.API.Endpoints
{
    public static class EditionEndpoints
    {
        public static void MapEditionEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/edition").WithTags("Edition");

            group.MapGet("/", async (IEditionService service) =>
            {
                return Results.Ok(await service.GetAllAsync());
            }).AllowAnonymous();

            group.MapGet("/active", async (IEditionService service) =>
            {
                var found = await service.GetActiveAsync();
                return found is not null ? Results.Ok(found) : Results.NotFound();
            }).AllowAnonymous();

            group.MapGet("/{id:guid}", async (Guid id, IEditionService service) =>
            {
                var found = await service.GetByIdAsync(id);
                return found is not null ? Results.Ok(found) : Results.NotFound();
            }).AllowAnonymous();

            group.MapPost("/", async (EditionCreateDto dto, IEditionService service) =>
            {
                var created = await service.CreateAsync(dto);
                return Results.Created($"/api/edition/{created.Id}", created);
            });

            group.MapPut("/{id:guid}", async (Guid id, EditionDto dto, IEditionService service) =>
            {
                var updated = await service.UpdateAsync(id, dto);
                return updated is not null ? Results.Ok(updated) : Results.NotFound();
            });

            group.MapPut("/{id:guid}/activate", async (Guid id, IEditionService service) =>
            {
                var activated = await service.ActivateAsync(id);
                return activated is not null ? Results.Ok(activated) : Results.NotFound();
            });

            group.MapDelete("/{id:guid}", async (Guid id, IEditionService service) =>
            {
                var success = await service.DeleteAsync(id);
                return success ? Results.Ok() : Results.NotFound();
            });
        }
    }
}
