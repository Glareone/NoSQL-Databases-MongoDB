using Microsoft.AspNetCore.Routing;

namespace MongoDB.WebAPI;

public interface IEndpointRouteHandler
{
    void MapEndpoints(IEndpointRouteBuilder app);
}