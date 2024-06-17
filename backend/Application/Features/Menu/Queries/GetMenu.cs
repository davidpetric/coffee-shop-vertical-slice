namespace Application.Features.Menu.Queries;

using Application.Domain.Products.ValueObjects;
using Application.Infrastructure.Endpoints;
using Application.Infrastructure.Persistence;

using System.Diagnostics.CodeAnalysis;
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

    public static Results<Ok<List<GetMenuProductResponse>>, NoContent> GetMenuProducts(
        [NotNull] CoffeeShopDbContext coffeesDb
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
                        ProductType.FromValue(x.ProductTypeId).Name
                    )
            )
            .ToList();

        return TypedResults.Ok(products);
    }
}

public record GetMenuProductResponse(long Id, string Name, string Dsc);
