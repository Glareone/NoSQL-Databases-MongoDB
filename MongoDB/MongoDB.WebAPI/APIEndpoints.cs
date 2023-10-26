using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using MongoDB.WebAPI.Entities;
using MongoDB.WebAPI.Service;

namespace MongoDB.WebAPI;

public class APIEndpoints : IEndpointRouteHandler
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
            .WithTags("Course Tag");
    }
}