namespace Application.Features.Users.Queries;

using Application.Infrastructure.Persistence;

using Carter;

using MediatR;

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public class GetUsers : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/users", (ISender sender) => sender.Send(new GetUsersQuery()))
            .Produces<List<GetUserResponse>>()
            .WithTags("users");
    }

    public record GetUsersQuery() : IRequest<IResult>;

    public record GetUserResponse(long Id, string DisplayName, string[] Roles, string EmailAddress);

    public class GetUsersQueryRequestHandler(CoffeeShopDbContext coffeesDb) : IRequestHandler<GetUsersQuery, IResult>
    {
        public async Task<IResult> Handle(
            GetUsersQuery _,
            CancellationToken cancellationToken
        )
        {
            if (!await coffeesDb.Users.AnyAsync())
            {
                return TypedResults.NoContent();
            }

            List<GetUserResponse> users = await coffeesDb.Users.Select(x => new GetUserResponse(x.Id, x.GetDisplayName(), x.Roles.Select(x => x.Name).ToArray(), x.EmailAddress.Email)).ToListAsync();

            return TypedResults.Ok(users);
        }
    }
}
