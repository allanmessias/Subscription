using Subscription.Core.Application;
using Subscription.Core.Domain;

namespace Subscription.Application.UseCases
{
    public class SendSubscriptionUseCase : ISendSubscriptionUseCase
    {
        private readonly IMessagePublisher _messagePublisher;

        public SendSubscriptionUseCase(IMessagePublisher messagePublisher)
        {
            _messagePublisher = messagePublisher;
        }

        public async Task ExecuteAsync<T>(string routingKey, T message)
        {

            await _messagePublisher.PublishAsync(message, routingKey);
        }
    }
}
