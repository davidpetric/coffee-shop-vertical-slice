namespace Application.Features.Users.Queries;

using Application.Infrastructure.Endpoints;
using Application.Infrastructure.Persistence;

using MediatR;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public class GetUsers : IEndpointDefinition
{
    public void AddRoutes(IEndpointRouteBuilder builder)
    {
        builder.MapGet("users", (ISender sender) => sender.Send(new GetUsersQuery()))
               .Produces<List<GetUserResponse>>()
               .WithTags("users");
    }
}

public record GetUsersQuery() : IRequest<IResult>;

public record GetUserResponse(long Id, string DisplayName, string[] Roles, string EmailAddress);

public class GetUsersQueryRequestHandler(CoffeeShopDbContext coffeesDb) : IRequestHandler<GetUsersQuery, IResult>
{
    public async Task<IResult> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (!await coffeesDb.Users.AnyAsync(cancellationToken: cancellationToken))
        {
            return TypedResults.NoContent();
        }

        List<GetUserResponse> users = await coffeesDb.Users.Select(x => new GetUserResponse(x.Id, x.GetDisplayName(), x.Roles.Select(x => x.Name).ToArray(), x.EmailAddress.Email)).ToListAsync(cancellationToken: cancellationToken);

        return TypedResults.Ok(users);
    }
}
