using Subscription.Core.Domain;

namespace Subscription.Application;

public class SubscriptionPublisher
{
    private readonly IMessagePublisher _publisher;

    public SubscriptionPublisher(IMessagePublisher publisher)
    {
        _publisher = publisher;
    }

    public Task SendEventAsync<T>(T message, string routingKey, CancellationToken cancellationToken = default)
    {
        if (message is null) throw new ArgumentNullException(nameof(message));    

        return _publisher.PublishAsync(message, routingKey, cancellationToken);
    }
}
