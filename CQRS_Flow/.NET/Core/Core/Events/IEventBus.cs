namespace Core.Events;

public interface IEventBus
{
    Task Publish(object @event, CancellationToken ct);
}
