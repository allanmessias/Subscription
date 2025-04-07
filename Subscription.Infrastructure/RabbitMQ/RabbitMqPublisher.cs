using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using RabbitMQ.Client;
using Subscription.Core.Domain;

namespace Subscription.Infrastructure.RabbitMq
{
    public class RabbitMqPublisher : IMessagePublisher
    {
        private readonly IMessageBrokerConnection _connection;

        public RabbitMqPublisher(IMessageBrokerConnection connection)
        {
            _connection = connection;
        }

        public async Task PublishAsync<T>(string topic, T message, CancellationToken cancellationToken = default)
        {
            await _connection.ConnectAsync(cancellationToken);
            var channel = await _connection.CreateChannelAsync(cancellationToken);

            if (channel is null)
            {
                throw new InvalidOperationException("Channel is not created.");
            }

            await channel.QueueDeclareAsync(
                 queue: topic,
                 durable: false,
                 exclusive: false,
                 autoDelete: false,
                 arguments: null,
                 cancellationToken: cancellationToken
             );

            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(message));

            await channel.BasicPublishAsync(
                exchange: "",
                routingKey: topic,
                mandatory: false,
                body: body,
                cancellationToken: cancellationToken
            );

            await channel.CloseAsync();
        }
    }
}
