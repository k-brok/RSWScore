using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using RSW.Shared.Entities;
using RSW.Shared.Interfaces;
using System.Security.Claims;

namespace RSW.API.Endpoints
{
    public static class UserEndpoints
    {
        public static IEndpointRouteBuilder MapUserEndpoints(this IEndpointRouteBuilder endpoints)
        {
            var group = endpoints.MapGroup("/api/users")
                                 .RequireAuthorization().WithTags("Users");

            // GET: api/users (alleen admin)
            group.MapGet("/", async (IUserService userService) =>
            {
                var users = await userService.GetAllAsync();
                return Results.Ok(users);
            }).RequireAuthorization(policy => policy.RequireRole("Admin"));

            // GET: api/users/{id}
            group.MapGet("/{id}", async (string id, IUserService userService, ClaimsPrincipal user) =>
            {
                if (!IsCurrentUserOrAdmin(user, id))
                    return Results.Forbid();

                var result = await userService.GetByIdAsync(id);
                return result is not null ? Results.Ok(result) : Results.NotFound();
            });

            group.MapGet("/{id}/volunteerassignments", async (string id, IUserService userService, ClaimsPrincipal user) =>
            {
                if (!IsCurrentUserOrAdmin(user, id))
                    return Results.Forbid();

                var result = await userService.GetVolunteerAssignmentsAsync(id);
                return result is not null ? Results.Ok(result) : Results.NotFound();
            });

            group.MapGet("/{id}/roles", async (string id, IUserService userService, ClaimsPrincipal user) =>
            {
                if (!IsCurrentUserOrAdmin(user, id))
                    return Results.Forbid();

                var result = await userService.GetRolesAsync(id);
                return result is not null ? Results.Ok(result) : Results.NotFound();
            });

            // POST: api/users (alleen admin)
            group.MapPost("/", async (ApplicationUser model, string password, IUserService userService) =>
            {
                var created = await userService.CreateAsync(model,password);
                return Results.Created($"/api/users/{created.Id}", created);
            }).RequireAuthorization(policy => policy.RequireRole("Admin"));

            // PUT: api/users/{id}
            group.MapPut("/{id}", async (string id, ApplicationUser model, IUserService userService, ClaimsPrincipal user) =>
            {
                if (!IsCurrentUserOrAdmin(user, id))
                    return Results.Forbid();

                var updated = await userService.UpdateAsync(id, model);
                return updated is not null ? Results.Ok(updated) : Results.NotFound();
            });

            // DELETE: api/users/{id}
            group.MapDelete("/{id}", async (string id, IUserService userService, ClaimsPrincipal user) =>
            {
                if (!IsCurrentUserOrAdmin(user, id))
                    return Results.Forbid();

                var success = await userService.DeleteAsync(id);
                return success ? Results.NoContent() : Results.NotFound();
            });

            return endpoints;
        }

        private static bool IsCurrentUserOrAdmin(ClaimsPrincipal user, string userId)
        {
            var currentUserId = user.FindFirstValue(ClaimTypes.NameIdentifier);
            var isAdmin = user.IsInRole("Admin");
            return isAdmin || currentUserId == userId;
        }
    }
}
