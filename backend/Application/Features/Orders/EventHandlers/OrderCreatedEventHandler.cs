namespace Application.Features.Orders.EventHandlers;

using Application.Common;
using Application.Domain.Orders;

using MediatR;

using Microsoft.Extensions.Logging;

using System.Threading;
using System.Threading.Tasks;

internal class OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> logger)
    : INotificationHandler<DomainEventNotification<OrderCreatedEvent>>
{
    public Task Handle(
        DomainEventNotification<OrderCreatedEvent> notification,
        CancellationToken cancellationToken
    )
    {
        logger.LogWarning("New Order Created: {OrderCreatedEvent}", notification.DomainEvent);

        return Task.CompletedTask;
    }
}
