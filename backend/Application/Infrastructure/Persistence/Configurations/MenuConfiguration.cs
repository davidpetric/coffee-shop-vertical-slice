namespace Application.Infrastructure.Persistence.Configurations;

using Application.Domain.Menus;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

sealed class MenuConfiguration : IEntityTypeConfiguration<Menu>
{
    public void Configure(EntityTypeBuilder<Menu> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasMany(x => x.Products);
    }
}
