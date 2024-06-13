namespace Application.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;

using Npgsql;

public static class ConfigureServices
{
    public static IServiceCollection AddDb(
        this IServiceCollection services,
        string? sqlConnectionString
    )
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(sqlConnectionString);

        try
        {
            _ = new NpgsqlConnectionStringBuilder(sqlConnectionString);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Invalid SQL connection string: {0}", ex.Message);
            throw;
        }

        services.AddDbContext<CoffeeShopDbContext>(opt =>
        {
            opt.UseNpgsql(sqlConnectionString);
#if DEBUG
            opt.EnableSensitiveDataLogging();
#endif
        });
        return services;
    }
}
