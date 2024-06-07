namespace Application.Infrastructure.Services;

using Application.Common;

using MediatR;

using System.Threading.Tasks;

internal class DomainEventService(IPublisher publisher) : IDomainEventService
{
    public Task PublishAsync(DomainEvent domainEvent, CancellationToken cancellationToken)
    {
        return publisher.Publish(GetNotificationCorrespondingToDomainEvent(domainEvent), cancellationToken);
    }

    private static INotification GetNotificationCorrespondingToDomainEvent(DomainEvent domainEvent)
    {
        return (INotification)Activator.CreateInstance(
            typeof(DomainEventNotification<>).MakeGenericType(domainEvent.GetType()), domainEvent)!;
    }
}
