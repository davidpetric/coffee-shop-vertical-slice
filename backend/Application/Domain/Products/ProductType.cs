namespace Application.Domain.Products;

using CSharpFunctionalExtensions;

public class ProductType : Entity
{
    public required string Name { get; set; }

    public List<Product> Products { get; } = [];

    public void SetId(long id)
    {
        Id = id;
    }

}
