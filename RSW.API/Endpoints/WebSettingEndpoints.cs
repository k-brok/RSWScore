using Microsoft.EntityFrameworkCore;
using RSW.API.Data;
using RSW.Shared.Dto;
using RSW.Shared.Entities;
using RSW.Shared.Interfaces;

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

            WebSetting.MapPost("/", async (WebSettingCreateDto dto, IWebSettingService service) =>
            {
                var created = await service.CreateAsync(dto);
                return Results.Created($"/api/WebSetting/{created.Id}", created);
            });

            WebSetting.MapPut("/{id:guid}", async (Guid id, WebSettingDto dto, IWebSettingService service) =>
            {
                var updated = await service.UpdateAsync(id, dto);
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
