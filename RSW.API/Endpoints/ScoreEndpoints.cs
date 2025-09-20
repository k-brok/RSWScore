using Microsoft.EntityFrameworkCore;
using RSW.API.Data;
using RSW.Shared.Dto;
using RSW.Shared.Entities;
using RSW.Shared.Interfaces;

namespace RSW.API.Endpoints
{
    public static class ScoreEndpoints
    {
        public static void MapScoreEndpoints(this WebApplication app)
        {
            var Score = app.MapGroup("/api/Score").WithTags("Score");

            Score.MapGet("/", async (IScoreService service) =>
            {
                return Results.Ok(await service.GetAllAsync());
            }).AllowAnonymous();

            Score.MapGet("/{id:guid}", async (Guid id, IScoreService service) =>
            {
                var found = await service.GetByIdAsync(id);
                return found is not null ? Results.Ok(found) : Results.NotFound();
            }).AllowAnonymous();

            Score.MapPost("/", async (ScoreCreateDto dto, IScoreService service) =>
            {
                var created = await service.CreateAsync(dto);
                return Results.Created($"/api/Score/{created.Id}", created);
            });

            Score.MapPut("/{id:guid}", async (Guid id, ScoreDto dto, IScoreService service) =>
            {
                var updated = await service.UpdateAsync(id, dto);
                return updated is not null ? Results.Ok(updated) : Results.NotFound();
            });

            Score.MapDelete("/{id:guid}", async (Guid id, IScoreService service) =>
            {
                var success = await service.DeleteAsync(id);
                return success ? Results.Ok() : Results.NotFound();
            });
        }
    }
}
