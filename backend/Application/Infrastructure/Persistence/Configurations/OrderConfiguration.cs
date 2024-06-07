namespace Application.Infrastructure.Persistence.Configurations;

using Application.Domain.Orders;

using Microsoft.EntityFrameworkCore.Metadata.Builders;

class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.Products);

        builder.Ignore(x => x.DomainEvents);
    }
}
