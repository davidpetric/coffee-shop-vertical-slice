namespace Application.Infrastructure.Logging;

using Microsoft.Extensions.Logging;

internal static class LogEventIds
{
    public static readonly EventId NewOrderCreatedEvent = new(1, "NewOrderCreatedEvent");

    public static readonly EventId TransactionRollbackReason = new(2, "TransactionRollbackReason");
}
