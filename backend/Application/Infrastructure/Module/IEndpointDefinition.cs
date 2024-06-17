namespace Application.Infrastructure.Module;

using Microsoft.AspNetCore.Routing;

public interface IEndpointDefinition
{
    void AddRoutes(IEndpointRouteBuilder builder);
}
