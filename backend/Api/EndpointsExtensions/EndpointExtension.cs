namespace Api.EndpointsExtensions;

using Application.Infrastructure.Module;

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
                    && x.IsAssignableFrom(typeof(IEndpointDefinition))
            )
            .Select(x => ServiceDescriptor.Transient(typeof(IEndpointDefinition), x))
            .ToArray();

        services.TryAddEnumerable(serviceDescriptors);

        return services;
    }

    public static WebApplication RegisterEndpoints(this WebApplication builder)
    {
        foreach (
            IEndpointDefinition endpoint in builder.Services.GetRequiredService<
                IEnumerable<IEndpointDefinition>
            >()
        )
        {
            endpoint.AddRoute(builder);
        }

        return builder;
    }
}
