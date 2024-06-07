namespace Application.Infrastructure.Persistence;

using Application.Common;
using Application.Domain.Menus;
using Application.Domain.Orders;
using Application.Domain.Products;
using Application.Domain.Users;
using Application.Infrastructure.Services;

using Microsoft.EntityFrameworkCore;

using System.Reflection;

public class CoffeeShopDbContext(DbContextOptions<CoffeeShopDbContext> options, IDomainEventService domainEventService) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Employee> Employees => Set<Employee>();

    public DbSet<Menu> Menus => Set<Menu>();

    public DbSet<Order> Orders => Set<Order>();

    public DbSet<Product> Products => Set<Product>();

    public DbSet<ProductType> ProductTypes => Set<ProductType>();

    public DbSet<User> Users => Set<User>();

    public DbSet<Role> Roles => Set<Role>();

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        DomainEvent[] domainEvents = ChangeTracker.Entries<IHasDomainEvent>()
                .Select(x => x.Entity.DomainEvents)
                .SelectMany(x => x)
                .Where(x => !x.IsPublished)
                .ToArray();

        int saveChangesResult = await base.SaveChangesAsync(cancellationToken);

        await PublishDomainEventsAsync(domainEvents, cancellationToken);

        return saveChangesResult;
    }

    private async Task PublishDomainEventsAsync(DomainEvent[] domainEvents, CancellationToken cancellationToken)
    {
        foreach (DomainEvent @event in domainEvents)
        {
            @event.IsPublished = true;

            await domainEventService.PublishAsync(@event, cancellationToken);
        }
    }
}
