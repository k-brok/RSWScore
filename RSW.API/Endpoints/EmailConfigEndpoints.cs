using Microsoft.EntityFrameworkCore;
using RSW.API.Data;
using RSW.Shared.Entities;
using RSW.Shared.Interfaces;

namespace RSW.API.Endpoints
{
    public static class EmailConfigEndpoints
    {
        public static void MapEmailConfigEndpoints(this WebApplication app)
        {
            var EmailConfig = app.MapGroup("/api/EmailConfig").WithTags("EmailConfig");

            EmailConfig.MapGet("/", async (IEmailConfigService service) =>
            {
                return Results.Ok(await service.GetAllAsync());
            }).AllowAnonymous();

            EmailConfig.MapGet("/{id:guid}", async (Guid id, IEmailConfigService service) =>
            {
                var found = await service.GetByIdAsync(id);
                return found is not null ? Results.Ok(found) : Results.NotFound();
            }).AllowAnonymous();

            EmailConfig.MapGet("/name/{name}", async (string name, IEmailConfigService service) =>
            {
                var found = await service.GetByNameAsync(name);
                return found is not null ? Results.Ok(found) : Results.NotFound();
            }).AllowAnonymous();

            EmailConfig.MapPut("/{id:guid}", async (Guid id, EmailConfig model, IEmailConfigService service) =>
            {
                var updated = await service.UpdateAsync(id, model);
                return updated is not null ? Results.Ok(updated) : Results.NotFound();
            }).RequireAuthorization(policy => policy.RequireRole("Admin"));
        }
    }
}
