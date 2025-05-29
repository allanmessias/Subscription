using Subscription.Core.Interfaces;

namespace Subscription.Infrastructure;

public class RabbitMqPublisher
{
    private readonly IMessagePublisher _publisher;

    public RabbitMqPublisher(IMessagePublisher publisher)
    {
        _publisher = publisher;
    }

    public Task SendEventAsync<T>(T message, string routingKey, CancellationToken cancellationToken = default)
    {
        if (message is null) throw new ArgumentNullException(nameof(message));    

        return _publisher.PublishAsync(message, routingKey, cancellationToken);
    }
}
