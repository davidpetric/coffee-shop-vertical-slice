namespace Application.Features.Menu.Queries;

using Application.Domain.Products.ValueObjects;
using Application.Infrastructure.Module;
using Application.Infrastructure.Persistence;

using System.Linq;

public class GetMenu : IEndpointDefinition
{
    public void AddRoutes(IEndpointRouteBuilder builder)
    {
        builder
            .MapGet("menu", GetMenuProducts)
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

        List<GetMenuProductResponse> products = coffeesDb.Products
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
}

public record GetMenuProductResponse(long Id, string Name, string Dsc);
