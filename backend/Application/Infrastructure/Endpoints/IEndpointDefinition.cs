namespace Application.Infrastructure.Endpoints;

using Microsoft.AspNetCore.Routing;

public interface IEndpointDefinition
{
    void AddRoutes(IEndpointRouteBuilder builder);
}
