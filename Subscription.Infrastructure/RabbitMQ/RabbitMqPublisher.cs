using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using Subscription.Core.Domain;

namespace Subscription.Infrastructure.RabbitMq
{
    public class RabbitMqPublisher : IMessagePublisher, IAsyncDisposable
    {
        private readonly IMessageBrokerConnection _connection;
        private readonly ILogger<RabbitMqPublisher> _logger;
        private IChannel _channel;
        private bool _disposed;

        private const string ExchangeName = "subscription_exchange";

        public RabbitMqPublisher(IMessageBrokerConnection connection, ILogger<RabbitMqPublisher> logger)
        {
            _connection = connection;
            _logger = logger;
        }

        public async Task PublishAsync<T>(T message, string routingKey, CancellationToken cancellationToken = default)
        {
            if (_disposed) throw new ObjectDisposedException(nameof(RabbitMqPublisher));
            if (message is null) throw new ArgumentNullException(nameof(message));

            if (_channel is null || _channel.IsOpen)
            {
                _channel = await _connection.CreateChannelAsync(cancellationToken);
                await _channel.ExchangeDeclareAsync(exchange: ExchangeName, type: ExchangeType.Topic, durable: true);
            }

            var json = JsonSerializer.Serialize(message);
            var body = Encoding.UTF8.GetBytes(json);

            var props = new BasicProperties
            {
                Persistent = true,
                ContentType = "application/json",
            };

            var key = string.IsNullOrWhiteSpace(routingKey) ? typeof(T).Name : routingKey;


            await _channel.BasicPublishAsync(
                    exchange: ExchangeName,
                    routingKey: key,
                    basicProperties: props,
                    body: body,
                    mandatory: false
                );
        }

        public async ValueTask DisposeAsync()
        {
            if (_disposed) return;

            if (_channel is not null)
            {
                await _channel.CloseAsync();
                await _channel.DisposeAsync();
            }

            _disposed = true;
        } 
    }
}
