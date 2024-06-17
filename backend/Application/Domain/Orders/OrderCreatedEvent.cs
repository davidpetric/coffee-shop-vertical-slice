namespace Application.Domain.Orders;

using Application.Common;

public sealed class OrderCreatedEvent(Order order) : DomainEvent
{
    public Order Order { get; } = order;
}
