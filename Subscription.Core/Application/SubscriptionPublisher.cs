using Subscription.Core.Interfaces;

namespace Subscription.Infrastructure;

public class RabbitMqPublisher
{
    private readonly IMessagePublisher _publisher;

    public RabbitMqPublisher(IMessagePublisher publisher)
    {
        _publisher = publisher;
    }

    public Task SendEventAsync<T>(string topic, T message, CancellationToken cancellationToken = default)
    {
        return _publisher.PublishAsync(topic, message, cancellationToken);
    }
}
