using RabbitMQ.Client;
using Subscription.Core.Domain;

namespace Subscription.Infrastructure.RabbitMQ;

public class RabbitMqConnection : IMessageBrokerConnection
{
    private IConnection _connection;
    private readonly ConnectionFactory _factory;

    public RabbitMqConnection(string hostName = "localhost")
    {
        _options = options.Value;
        _factory = new ConnectionFactory { 
            HostName = _options.Host, 
            Port = _options.Port, 
            UserName = _options.UserName, 
            Password = _options.Password,
        };
    }

    public bool IsConnected => _connection?.IsOpen ?? false;

    public async Task ConnectAsync(CancellationToken cancellationToken = default)
    {
        if (IsConnected) return;


        _connection = await _factory.CreateConnectionAsync();
        await Task.CompletedTask;
    }

    public async Task DisconnectAsync(CancellationToken cancellationToken = default)
    {
        if (!IsConnected) return;

        _connection?.CloseAsync();
        _connection?.Dispose();
        await Task.CompletedTask;
    }

    public IConnection GetConnection() => _connection;

    public async Task<IChannel> CreateChannelAsync(CancellationToken cancellationToken = default)
    {
        if (!IsConnected)
        {
            await ConnectAsync(cancellationToken);
        }

        return await _connection!.CreateChannelAsync();
    }
}
