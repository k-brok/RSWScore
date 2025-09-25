using RSW.Shared.Entities;
using RSW.Shared.Interfaces;

namespace RSW.API.Endpoints
{
    public static class VolunteerAssignmentEndpoints
    {
        public static void MapVolunteerAssignmentEndpoints(this WebApplication app)
        {
            var VolunteerAssignment = app.MapGroup("/api/volunteerassignment").WithTags("VolunteerAssignment");

            VolunteerAssignment.MapGet("/", async (IVolunteerAssignmentService service) =>
            {
                return Results.Ok(await service.GetAllAsync());
            }).AllowAnonymous();

            VolunteerAssignment.MapGet("/{id:guid}", async (Guid id, IVolunteerAssignmentService service) =>
            {
                var found = await service.GetByIdAsync(id);
                return found is not null ? Results.Ok(found) : Results.NotFound();
            }).AllowAnonymous();

            VolunteerAssignment.MapPost("/", async (VolunteerAssignment model, IVolunteerAssignmentService service) =>
            {
                var created = await service.CreateAsync(model);
                return Results.Created($"/api/VolunteerAssignment/{created.Id}", created);
            });

            VolunteerAssignment.MapPut("/{id:guid}", async (Guid id, VolunteerAssignment model, IVolunteerAssignmentService service) =>
            {
                var updated = await service.UpdateAsync(id, model);
                return updated is not null ? Results.Ok(updated) : Results.NotFound();
            });

            VolunteerAssignment.MapDelete("/{id:guid}", async (Guid id, IVolunteerAssignmentService service) =>
            {
                var success = await service.DeleteAsync(id);
                return success ? Results.Ok() : Results.NotFound();
            });
        }
    }
}
