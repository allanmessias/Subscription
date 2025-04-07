namespace Subscription.Core.Domain;

public interface IMessageConsumer
{
	Task SubscribeAsync(string topic, Func<string, Task> handleMessage, CancellationToken cancellationToken = default);
}
