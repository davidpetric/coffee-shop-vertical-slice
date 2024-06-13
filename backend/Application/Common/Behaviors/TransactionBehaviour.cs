namespace Application.Common.Behaviors;

using Application.Infrastructure.Persistence;

using MediatR;

using Microsoft.Extensions.Logging;

using System;
using System.Threading;
using System.Threading.Tasks;

public class TransactionBehavior<TRequest, TResponse>(
    ILogger<TransactionBehavior<TRequest, TResponse>> logger,
    CoffeeShopDbContext dbContext) : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Major Code Smell", "S2139:Exceptions should be either logged or rethrown but not both", Justification = "<Pending>")]
    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken
    )
    {
        try
        {
            logger.LogInformation("Begin sql transaction.");

            await dbContext.Database.BeginTransactionAsync();

            TResponse? response = await next();

            await dbContext.Database.CommitTransactionAsync();

            return response;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Rollback, {ExceptionMessage}", ex.Message);
            await dbContext.Database.RollbackTransactionAsync();
            throw;
        }
    }
}