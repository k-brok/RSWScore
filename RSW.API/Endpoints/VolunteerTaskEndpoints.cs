using RSW.Shared.Entities;
using RSW.Shared.Interfaces;

namespace RSW.API.Endpoints
{
    public static class VolunteerTaskEndpoints
    {
        public static void MapVolunteerTaskEndpoints(this WebApplication app)
        {
            var VolunteerTask = app.MapGroup("/api/volunteertask").WithTags("VolunteerTask");

            VolunteerTask.MapGet("/", async (IVolunteerTaskService service) =>
            {
                return Results.Ok(await service.GetAllAsync());
            }).AllowAnonymous();

            VolunteerTask.MapGet("/{id:guid}", async (Guid id, IVolunteerTaskService service) =>
            {
                var found = await service.GetByIdAsync(id);
                return found is not null ? Results.Ok(found) : Results.NotFound();
            }).AllowAnonymous();

            VolunteerTask.MapPost("/", async (VolunteerTask model, IVolunteerTaskService service) =>
            {
                var created = await service.CreateAsync(model);
                return Results.Created($"/api/VolunteerTask/{created.Id}", created);
            });

            VolunteerTask.MapPut("/{id:guid}", async (Guid id, VolunteerTask model, IVolunteerTaskService service) =>
            {
                var updated = await service.UpdateAsync(id, model);
                return updated is not null ? Results.Ok(updated) : Results.NotFound();
            });

            VolunteerTask.MapDelete("/{id:guid}", async (Guid id, IVolunteerTaskService service) =>
            {
                var success = await service.DeleteAsync(id);
                return success ? Results.Ok() : Results.NotFound();
            });
        }
    }
}
