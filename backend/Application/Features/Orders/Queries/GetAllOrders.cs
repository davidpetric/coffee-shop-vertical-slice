namespace Application.Features.Orders.Queries;

using Application.Infrastructure.Persistence;

using Carter;

using MediatR;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public class GetAllOrders : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app
          .MapGet("api/orders", (ISender sender) => sender.Send(new GetAllOrdersQuery()))
          .WithTags("orders")
          .WithOpenApi();
    }

    public record GetAllOrdersQuery() : IRequest<List<GetOrdersResponse>>;

    public record GetOrdersResponse(long OrderId, decimal TotalPrice);

    public sealed class GetAllOrdersQueryHandler(CoffeeShopDbContext dbContext) : IRequestHandler<GetAllOrdersQuery, List<GetOrdersResponse>>
    {
        public async Task<List<GetOrdersResponse>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            List<GetOrdersResponse> orders = await dbContext.Orders
                .Select(x => new GetOrdersResponse(x.Id, x.TotalPrice))
                .ToListAsync(cancellationToken);

            return orders;
        }
    }
}
