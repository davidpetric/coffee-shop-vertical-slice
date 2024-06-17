namespace Application.Common;

using MediatR;

public class DomainEventNotification<TDomainEvent>(TDomainEvent domainEvent)
    : INotification
    where TDomainEvent : DomainEvent
{
    public TDomainEvent DomainEvent { get; } = domainEvent;
}