namespace Application.Infrastructure.Services;

using Application.Common;

using System.Threading.Tasks;

public interface IDomainEventService
{
    Task PublishAsync(DomainEvent domainEvent, CancellationToken cancellationToken);
}
