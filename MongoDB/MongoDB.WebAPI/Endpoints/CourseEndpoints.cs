using Microsoft.AspNetCore.Mvc;
using MongoDB.WebAPI.Entities;
using MongoDB.WebAPI.Service;

namespace MongoDB.WebAPI.Endpoints;

public class CourseEndpoints : IEndpointRouteHandler
{
    public void MapEndpoints(IEndpointRouteBuilder app)
    {
        // Use Results.Problem to enrich IETF output.
        // Quite popular way to solve the problem
        app.MapGet("/problems", () => Results.Problem(detail: "This will end up in the 'detail' field."))
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .WithName("Problems");
        
        app.MapGet("/courses", async (CourseService courseService) => await courseService.GetAsync())
            .Produces<IReadOnlyCollection<Course>>()
            .WithName("Courses")
            .WithTags("Courses");
        
        app.MapPut("/course", async (string oldName, string newName, CourseService courseService) =>
            {
                var isUpdated = await courseService.UpdateAsync(oldName.Trim(), newName.Trim());
                return isUpdated;
            }).Produces<bool>()
            .WithName("Course")
            .WithTags("Courses");
    }
}