namespace Application.Domain.Menus;

using Application.Domain.Products;

using CSharpFunctionalExtensions;

public class Menu : Entity
{
    public bool IsActive { get; set; }

    public List<Product> Products { get; } = [];
}
