using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RSW.Infrastructure.Services;
using RSW.Domain.Dto;
using RSW.Application.Interfaces;

namespace RSW.API.Endpoints;

public static class UnitLinkEndpoints
{
    public static void MapUnitLinkEndpoints(this WebApplication app)
    {
        var group = app.MapGroup("/api/unit-links")
                       .WithTags("Unit Links")
                       .RequireAuthorization();

        group.MapGet("/", async (IUnitLinkRequestService workflow) =>
        {
            var Requests = await workflow.GetAllAsync();
            return Results.Ok(Requests);
        }).RequireAuthorization(policy => policy.RequireRole("Admin"));

        group.MapPost("/request", async (
            [FromBody] CreateUnitLinkRequestRequest req,
            IUnitLinkRequestService workflow) =>
        {
            var result = await workflow.CreateAsync(req);
            return result.Success ? Results.Ok(result) : Results.BadRequest(result);
        });

        // Eigen aanvragen ophalen
        group.MapGet("/mine", async (IUnitLinkRequestService workflow) =>
        {
            var list = await workflow.GetMineAsync();
            return Results.Ok(list);
        });

        // Aanvraag annuleren (door aanvrager)
        group.MapPost("/cancel/{id:guid}", async (Guid id, IUnitLinkRequestService workflow) =>
        {
            var ok = await workflow.CancelAsync(id);
            return ok ? Results.Ok() : Results.BadRequest();
        });

        group.MapPost("/approve/{id:guid}", async (Guid id, IUnitLinkRequestService workflow) =>
        {
            var ok = await workflow.ApproveAsync(id);
            return ok ? Results.Ok() : Results.BadRequest();
        }).RequireAuthorization(policy => policy.RequireRole("Admin"));;

        group.MapPost("/reject/{id:guid}", async (Guid id, IUnitLinkRequestService workflow) =>
        {
            var ok = await workflow.RejectAsync(id);
            return ok ? Results.Ok() : Results.BadRequest();
        }).RequireAuthorization(policy => policy.RequireRole("Admin"));;
    }
}
