namespace Application.Features.Orders.Commands;

using Application.Domain.Orders;
using Application.Domain.Products;
using Application.Infrastructure.Persistence;

using Carter;
using Carter.ModelBinding;

using FluentValidation;
using FluentValidation.Results;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public class AddNewOrder : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app
          .MapPost("api/orders", (ISender sender, [FromBody] AddNewOrderCommand command) => sender.Send(command))
          .ProducesValidationProblem()
          .WithTags("orders");
    }

    public record AddNewOrderCommand(ICollection<long> ProductIds) : IRequest<IResult>;

    public class AddNewOrderCommandValidator : AbstractValidator<AddNewOrderCommand>
    {
        public AddNewOrderCommandValidator()
        {
            RuleFor(x => x.ProductIds).NotNull();
        }
    }

    public sealed class AddNewOrderCommandHandler(CoffeeShopDbContext dbContext, IValidator<AddNewOrderCommand> validator) : IRequestHandler<AddNewOrderCommand, IResult>
    {
        public async Task<IResult> Handle(AddNewOrderCommand request, CancellationToken cancellationToken)
        {
            ValidationResult result = await validator.ValidateAsync(request, cancellationToken);
            if (!result.IsValid)
            {
                return Results.ValidationProblem(result.GetValidationProblems());
            }

            List<Product> products = await dbContext.Products.Where(x => x.Id > 0).ToListAsync();

            Order order = new()
            {
                Products = products,
            };

            order.DomainEvents.Add(new OrderCreatedEvent(order));

            dbContext.Orders.Add(order);

            await dbContext.SaveChangesAsync(cancellationToken);

            return Results.Created($"api/orders/{order.Id}", null);
        }
    }
}
