namespace Application.Domain.Products;

using CSharpFunctionalExtensions;

public class Product : Entity
{
    public required string Name { get; set; }

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public bool IsDeleted { get; set; }

    public long ProductTypeId { get; set; }

    public ProductType ProductType { get; set; } = default!;
}