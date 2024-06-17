namespace Application.Features.Products.Queries;

using Application.Domain.Products.ValueObjects;
using Application.Infrastructure.Endpoints;
using Application.Infrastructure.Persistence;

using MediatR;

using Microsoft.AspNetCore.Routing;

using System.Threading;
using System.Threading.Tasks;

public class GetProducts : IEndpointDefinition
{
    public void AddRoutes(IEndpointRouteBuilder builder)
    {
        builder
            .MapGet("products", (ISender sender) => sender.Send(new GetProductsQuery()))
            .WithTags("products");
    }
}

public record GetProductsQuery() : IRequest<List<GetProductResponse>>;

public record GetProductResponse(long Id, string Name, string ProductType);

public sealed class GetProductsQueryHandler(CoffeeShopDbContext context)
    : IRequestHandler<GetProductsQuery, List<GetProductResponse>>
{
    public Task<List<GetProductResponse>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
    {
        return context.Products
            .Select(x => new GetProductResponse(x.Id, x.Name, ProductType.FromValue(x.ProductTypeId).Name))
            .ToListAsync(cancellationToken);
    }
}
