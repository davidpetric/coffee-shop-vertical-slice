namespace Application.Domain.Orders;

using Application.Common;

internal sealed class OrderCreatedEvent(Order order) : DomainEvent
{
    public Order Order { get; } = order;
}
