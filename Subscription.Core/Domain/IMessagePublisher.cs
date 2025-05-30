namespace Subscription.Core.Domain;

public interface IMessagePublisher
{
	Task PublishAsync<T>(T message, string routingKey = null, CancellationToken cancellationToken = default);
}
