namespace Subscription.Core.Domain;

public interface IMessagePublisher
{
	Task PublishAsync<T>(string topic, T message, CancellationToken cancellationToken = default);
}
