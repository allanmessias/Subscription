using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Subscription.Core.Domain;
using System.Text;

namespace Subscription.Consumer;

public class SubscriptionWorker : BackgroundService
{
    private readonly ILogger<SubscriptionWorker> _logger;
    private readonly IMessageBrokerConnection _connection;
    private IChannel _channel;

    private const string ExchangeName = "subscription_exchange";
    private const string QueueName = "subscription_created";
    private const string RoutingKey = "subscription.ascan";

    public SubscriptionWorker(ILogger<SubscriptionWorker> logger, IMessageBrokerConnection connection)
    {
        _logger = logger;
        _connection = connection;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _channel = await _connection.CreateChannelAsync(stoppingToken);

        await _channel.ExchangeDeclareAsync(ExchangeName, ExchangeType.Topic, durable: true, cancellationToken: stoppingToken);
        await _channel.QueueDeclareAsync(QueueName, durable: true, exclusive: false, autoDelete: false, cancellationToken: stoppingToken);
        await _channel.QueueBindAsync(QueueName, ExchangeName, RoutingKey, cancellationToken: stoppingToken);

        var consumer = new AsyncEventingBasicConsumer(_channel);

        consumer.ReceivedAsync += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            _logger.LogInformation("📥 Message received with success: {Message}", message);

            await Task.Yield(); 
        };

        await _channel.BasicConsumeAsync(queue: QueueName, autoAck: true, consumer: consumer);

        _logger.LogInformation("🟢 Consumer Initialized with success.");
    }

    public override async Task StopAsync(CancellationToken cancellationToken)
    {
        if (_channel != null)
        {
            await _channel.CloseAsync();
            await _channel.DisposeAsync();
        }

        await base.StopAsync(cancellationToken);
    }
}
