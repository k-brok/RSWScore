using Microsoft.EntityFrameworkCore;
using RSW.API.Data;
using RSW.Shared.Dto;
using RSW.Shared.Entities;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RSW.API.Endpoints
{
    public static class PatrolEndpoints
    {
        public static void MapPatrolEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api/activities").WithTags("Patrol");

            group.MapGet("/", async (AppDbContext Context) =>
            {
                var all = await Context.Patrols.ToListAsync();
                return Results.Ok(all.Select(A => A.ToDto()));
            }).AllowAnonymous();

            group.MapGet("/{id:guid}", async (Guid id, AppDbContext Context) =>
            {
                var patrol = await Context.Patrols.FirstOrDefaultAsync(A => A.Id == id);
                return patrol is not null ? Results.Ok(patrol.ToDto()) : Results.NotFound();
            }).AllowAnonymous();

            group.MapPost("/", async (PatrolDto patrol, AppDbContext Context) =>
            {
                var created = Context.Patrols.Add(patrol.ToEntity());
                await Context.SaveChangesAsync();
                return Results.Created($"/api/activities/{created.Entity.Id}", created);
            });

            group.MapPut("/{id:guid}", async (Guid id, PatrolDto updated, AppDbContext Context) =>
            {
                Patrol? result = await Context.Patrols.FirstOrDefaultAsync(A => A.Id == id);
                if (result == null)
                    return Results.NotFound();

                result.Id = updated.Id;
                result.Name = updated.Name;
                result.Number = updated.Number;
                result.SubGroupId = updated.SubGroupId;
                result.GroupId = updated.GroupId;
                result.TotalScore = updated.TotalScore;
                result.position = updated.position;
                result.IsYoungest = updated.IsYoungest;

                await Context.SaveChangesAsync();
                return Results.Ok(result);
            });

            group.MapDelete("/{id:guid}", async (Guid id, AppDbContext Context) =>
            {
                var FoundPatrol = await Context.Patrols.FirstOrDefaultAsync(A => A.Id == id);
                if (FoundPatrol == null)
                    return Results.NotFound();

                var success = Context.Patrols.Remove(FoundPatrol);
                await Context.SaveChangesAsync();
                return Results.Ok();
            });
        }
    }
}
