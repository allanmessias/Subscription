using Subscription.Core.Domain;

namespace Subscription.Application;

public class SubscriptionPublisher
{
    private readonly IMessagePublisher _publisher;

    public SubscriptionPublisher(IMessagePublisher publisher)
    {
        _publisher = publisher;
    }

    public Task SendEventAsync<T>(string topic, T message, CancellationToken cancellationToken = default)
    {
        return _publisher.PublishAsync(topic, message, cancellationToken);
    }
}
