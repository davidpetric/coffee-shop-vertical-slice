namespace Application.Features.Orders.EventHandlers;

using Application.Common;
using Application.Domain.Orders;

using MediatR;

using Microsoft.Extensions.Logging;

using System.Threading;
using System.Threading.Tasks;

public partial class OrderCreatedHandler(ILogger<OrderCreatedHandler> logger)
    : INotificationHandler<DomainEventNotification<OrderCreatedEvent>>
{
    private readonly ILogger _logger = logger;

    public Task Handle(
        DomainEventNotification<OrderCreatedEvent> notification,
        CancellationToken cancellationToken
    )
    {
        LogNewOrderCreated(notification.DomainEvent.Order);

        return Task.CompletedTask;
    }

    [LoggerMessage(0, LogLevel.Information, "New order created {order}")]
    partial void LogNewOrderCreated(Order order);
}
