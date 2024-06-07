namespace Application.Infrastructure.Persistence.Configurations;

using Application.Domain.Products;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name).HasColumnType("varchar").HasMaxLength(50).IsRequired();

        builder.Property(x => x.Description).HasColumnType("text");

        builder
            .HasOne(x => x.ProductType)
            .WithMany(x => x.Products)
            .HasForeignKey(x => x.ProductTypeId)
            .IsRequired();
    }
}
