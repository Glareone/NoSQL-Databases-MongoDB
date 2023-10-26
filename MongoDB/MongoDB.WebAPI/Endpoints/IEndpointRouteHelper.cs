namespace MongoDB.WebAPI.Endpoints;

public interface IEndpointRouteHandler
{
    void MapEndpoints(IEndpointRouteBuilder app);
}