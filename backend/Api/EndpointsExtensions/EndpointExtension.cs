namespace Api.EndpointsExtensions;

using Application.Infrastructure.Endpoints;

using Microsoft.Extensions.DependencyInjection.Extensions;

using System.Reflection;

public static class EndpointExtension
{
    public static IServiceCollection AddEndpoints(
        this IServiceCollection services,
        Assembly assembly
    )
    {
        ServiceDescriptor[] serviceDescriptors = assembly.DefinedTypes
            .Where(
                x =>
                    x is { IsAbstract: false, IsInterface: false }
                    && x.ImplementedInterfaces.Any(x => x == typeof(IEndpointDefinition))
            )
            .Select(x => ServiceDescriptor.Transient(typeof(IEndpointDefinition), x))
            .ToArray();

        services.TryAddEnumerable(serviceDescriptors);

        return services;
    }

    public static WebApplication RegisterEndpoints(this WebApplication app)
    {
        foreach (
            IEndpointDefinition endpointDefinition in app.Services.GetRequiredService<
                IEnumerable<IEndpointDefinition>
            >()
        )
        {
            endpointDefinition.AddRoutes(app);
        }

        return app;
    }
}
