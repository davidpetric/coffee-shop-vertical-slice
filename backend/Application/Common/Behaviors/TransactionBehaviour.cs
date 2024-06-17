namespace Application.Common.Behaviors;
using Application.Infrastructure.Persistence;

using MediatR;

using Microsoft.Extensions.Logging;

using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

public partial class TransactionBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly ILogger<TransactionBehavior<TRequest, TResponse>> logger;
    private readonly CoffeeShopDbContext dbContext;

    public TransactionBehavior(
        ILogger<TransactionBehavior<TRequest, TResponse>> logger,
        CoffeeShopDbContext dbContext
    )
    {
        this.logger = logger;
        this.dbContext = dbContext;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        [NotNull] RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken
    )
    {
        try
        {
            await dbContext.Database.BeginTransactionAsync(cancellationToken);

            TResponse? response = await next();

            await dbContext.Database.CommitTransactionAsync(cancellationToken);

            return response;
        }
        catch (Exception ex)
        {
            LogTransactionRollbackReason(ex);

            await dbContext.Database.RollbackTransactionAsync(cancellationToken);
            throw;
        }
    }

    [LoggerMessage(0, LogLevel.Error, "Sql transaction failed because: {ExceptionReason}")]
    [SuppressMessage(
        "LoggingGenerator",
        "SYSLIB1013:Don't include exception parameters as templates in the logging message",
        Justification = "<Pending>"
    )]
    partial void LogTransactionRollbackReason(Exception exceptionReason);
}
