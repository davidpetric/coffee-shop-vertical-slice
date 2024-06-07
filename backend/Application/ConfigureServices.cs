namespace Application;

using Application.Common.Behaviors;
using Application.Infrastructure.Persistence;
using Application.Infrastructure.Services;

using FluentValidation;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

public static class ConfigureServices
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddDb(configuration.GetConnectionString("SqlConnectionString"));

        services.AddValidatorsFromAssembly(typeof(ConfigureServices).Assembly);

        services.AddMediatR(opt =>
        {
            opt.RegisterServicesFromAssemblies(typeof(ConfigureServices).Assembly);

            opt.AddOpenBehavior(typeof(TransactionBehavior<,>));
        });

        services.AddScoped<IDomainEventService, DomainEventService>();

        return services;
    }
}
