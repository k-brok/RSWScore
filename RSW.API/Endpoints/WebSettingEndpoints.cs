using Microsoft.EntityFrameworkCore;
using RSW.Infrastructure.Data;
using RSW.Domain.Entities;
using RSW.Application.Interfaces;

namespace RSW.API.Endpoints
{
    public static class WebSettingEndpoints
    {
        public static void MapWebSettingEndpoints(this WebApplication app)
        {
            var WebSetting = app.MapGroup("/api/Setting").WithTags("Settings");

            WebSetting.MapGet("/", async (IWebSettingService service) =>
            {
                return Results.Ok(await service.GetAllAsync());
            }).AllowAnonymous();

            WebSetting.MapGet("/{id:guid}", async (Guid id, IWebSettingService service) =>
            {
                var found = await service.GetByIdAsync(id);
                return found is not null ? Results.Ok(found) : Results.NotFound();
            }).AllowAnonymous();

            WebSetting.MapPost("/", async (WebSetting model, IWebSettingService service) =>
            {
                var created = await service.CreateAsync(model);
                return Results.Created($"/api/WebSetting/{created.Id}", created);
            });

            WebSetting.MapPut("/{id:guid}", async (Guid id, WebSetting model, IWebSettingService service) =>
            {
                var updated = await service.UpdateAsync(id, model);
                return updated is not null ? Results.Ok(updated) : Results.NotFound();
            });

            WebSetting.MapDelete("/{id:guid}", async (Guid id, IWebSettingService service) =>
            {
                var success = await service.DeleteAsync(id);
                return success ? Results.Ok() : Results.NotFound();
            });
        }
    }
}
