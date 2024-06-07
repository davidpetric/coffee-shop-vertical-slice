namespace Application.Features.Menu.Queries;

using Application.Domain.Products.ValueObjects;
using Application.Infrastructure.Persistence;

using Carter;

using System.Linq;

public class GetMenu : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/menu", GetMenuProducts)
            .Produces<List<GetMenuProductResponse>>()
            .WithTags("menu")
            .WithDescription("Gets the shop menu.")
            .WithOpenApi();
    }

    public Results<Ok<List<GetMenuProductResponse>>, NoContent> GetMenuProducts(
        CoffeeShopDbContext coffeesDb
    )
    {
        if (!coffeesDb.Menus.Any())
        {
            return TypedResults.NoContent();
        }

        var products = coffeesDb.Products
            .Select(
                x =>
                    new GetMenuProductResponse(
                        x.Id,
                        x.Name,
                        ProductTypeEnum.FromValue(x.ProductTypeId).Name
                    )
            )
            .ToList();

        return TypedResults.Ok(products);
    }

    public record GetMenuProductResponse(long Id, string Name, string Dsc);
}
