namespace Application.Features.Products.Commands;

using Application.Domain.Products;
using Application.Domain.Products.ValueObjects;
using Application.Infrastructure.Module;
using Application.Infrastructure.Persistence;

using FluentValidation;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

using System.Threading;
using System.Threading.Tasks;

public class AddNewProduct : IEndpointDefinition
{
    public void AddRoutes(IEndpointRouteBuilder builder)
    {
        builder.MapPost("products", (ISender sender, [FromBody] AddNewProductCommand command) => sender.Send(command))
               .WithTags("products");
    }
}

public record AddNewProductCommand(string Name, string Description, decimal Price, long ProductTypeId) : IRequest<IResult>;

public class AddNewProductCommandValidator : AbstractValidator<AddNewProductCommand>
{ }

public class AddNewProductCommandHandler(CoffeeShopDbContext dbContext)
    : IRequestHandler<AddNewProductCommand, IResult>
{
    public async Task<IResult> Handle(AddNewProductCommand request, CancellationToken cancellationToken)
    {
        Product product = new()
        {
            Name = request.Name,
            Description = request.Description,
            Price = request.Price,
            ProductTypeId = ProductTypeEnum.FromValue(request.ProductTypeId),
        };

        dbContext.Add(product);

        await dbContext.SaveChangesAsync();

        return Results.CreatedAtRoute($"products/{product.Id}", null);
    }
}