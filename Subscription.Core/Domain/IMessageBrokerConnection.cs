using RabbitMQ.Client;

namespace Subscription.Core.Domain;
public interface IMessageBrokerConnection
{
	Task ConnectAsync(CancellationToken cancellationToken = default);
	Task DisconnectAsync(CancellationToken cancellationToken = default);
    Task<IChannel> CreateChannelAsync(CancellationToken cancellationToken = default);
    bool IsConnected { get; }
}
