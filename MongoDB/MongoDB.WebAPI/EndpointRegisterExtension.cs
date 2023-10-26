using System;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Routing;

namespace MongoDB.WebAPI;

// Using Reflection we may find all registered IEndpointRouteHandlers like PeopleHelper and register them all
public static class EndpointRouteBuilderExtensions
{
    public static void MapAllEndpoints(this
        IEndpointRouteBuilder app, Assembly assembly)
    {
        var endpointRouteHandlerInterfaceType =
            typeof(IEndpointRouteHandler);
        var endpointRouteHandlerTypes =
            assembly.GetTypes().Where(t =>
                t.IsClass && !t.IsAbstract && !t.IsGenericType
                && t.GetConstructor(Type.EmptyTypes) != null
                && endpointRouteHandlerInterfaceType
                    .IsAssignableFrom(t));
        foreach (var endpointRouteHandlerType in
                 endpointRouteHandlerTypes)
        {
            var instantiatedType = (IEndpointRouteHandler)
                Activator.CreateInstance
                    (endpointRouteHandlerType)!;
            instantiatedType.MapEndpoints(app);
        }
    }
}