using Microsoft.EntityFrameworkCore;
using RSW.API.Data;
using RSW.Shared.Dto;
using RSW.Shared.Entities;
using RSW.Shared.Interfaces;

namespace RSW.API.Endpoints
{
    public static class GroupEndpoints
    {
        public static void MapGroupEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/Group").WithTags("Group");

            group.MapGet("/", async (IGroupService service) =>
            {
                return Results.Ok(await service.GetAllAsync());
            }).AllowAnonymous();

            group.MapGet("/{id:guid}", async (Guid id, IGroupService service) =>
            {
                var found = await service.GetByIdAsync(id);
                return found is not null ? Results.Ok(found) : Results.NotFound();
            }).AllowAnonymous();

            group.MapPost("/", async (GroupCreateDto dto, IGroupService service) =>
            {
                var created = await service.CreateAsync(dto);
                return Results.Created($"/api/Group/{created.Id}", created);
            });

            group.MapPut("/{id:guid}", async (Guid id, GroupDto dto, IGroupService service) =>
            {
                var updated = await service.UpdateAsync(id, dto);
                return updated is not null ? Results.Ok(updated) : Results.NotFound();
            });

            group.MapDelete("/{id:guid}", async (Guid id, IGroupService service) =>
            {
                var success = await service.DeleteAsync(id);
                return success ? Results.Ok() : Results.NotFound();
            });

            group.MapGet("/{id:guid}/patrols", async (Guid id, IGroupService service) =>
            {
                return Results.Ok(await service.GetPatrolsAsync(id));
            }).AllowAnonymous();
        }
    }
}
