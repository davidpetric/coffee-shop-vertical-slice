namespace Application.Common;

public abstract class DomainEvent
{
    protected DomainEvent()
    {
        Timestamp = DateTimeOffset.UtcNow;
    }

    public bool IsPublished { get; set; }

    public DateTimeOffset Timestamp { get; protected set; }
}
