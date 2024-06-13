namespace Application;

using Application.Common.Behaviors;
using Application.Infrastructure.Persistence;
using Application.Infrastructure.Services;

using FluentValidation;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using System.Reflection;

public static class ConfigureApplicationServices
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        Assembly assembly = typeof(ConfigureApplicationServices).Assembly;

        services.AddDb(configuration.GetConnectionString("SqlConnectionString"));

        services.AddValidatorsFromAssembly(assembly);

        services.AddMediatR(opt =>
        {
            opt.RegisterServicesFromAssemblies(assembly);

            opt.AddOpenBehavior(typeof(TransactionBehavior<,>));
        });

        services.AddScoped<IDomainEventService, DomainEventService>();

        return services;
    }
}
