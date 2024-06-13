namespace Application.Infrastructure.Module;

using Microsoft.AspNetCore.Routing;

public interface IEndpointDefinition
{
    void AddRoute(IEndpointRouteBuilder builder);
}
