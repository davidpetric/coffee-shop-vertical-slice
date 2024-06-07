namespace Application.Features.Products.Queries;

using Application.Domain.Products.ValueObjects;
using Application.Infrastructure.Persistence;

using Carter;

using MediatR;

using Microsoft.AspNetCore.Routing;

using System.Threading;
using System.Threading.Tasks;

public class GetProducts : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("api/products", (ISender sender) => sender.Send(new GetProductsQuery()))
            .WithTags("products");
    }

    public record GetProductsQuery() : IRequest<List<GetProductResponse>>;

    public record GetProductResponse(long Id, string Name, string ProductType);

    public sealed class GetProductsQueryHandler(CoffeeShopDbContext context)
        : IRequestHandler<GetProductsQuery, List<GetProductResponse>>
    {
        public Task<List<GetProductResponse>> Handle(
            GetProductsQuery _,
            CancellationToken cancellationToken
        )
        {

            //TODO: read from cache first.
            return context.Products.Select(x => new GetProductResponse(x.Id, x.Name, ProductTypeEnum.FromValue(x.ProductTypeId).Name)).ToListAsync(cancellationToken);
        }
    }
}
