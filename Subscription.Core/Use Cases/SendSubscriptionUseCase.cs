using Subscription.Core.Application;
using Subscription.Core.Domain

namespace Subscription.Application.UseCases
{
    public class SendSubscriptionUseCase : ISendSubscriptionUseCase
    {
        private readonly IMessagePublisher _messagePublisher;

        public SendSubscriptionUseCase(IMessagePublisher messagePublisher)
        {
            _messagePublisher = messagePublisher;
        }

        public async Task ExecuteAsync(SubscriptionUpdatedEvent subscription, CancellationToken cancellationToken = default)
        {
            if (string.IsNullOrEmpty(subscription.Email))
                throw new ArgumentException("Email inválido.");

            await _messagePublisher.PublishAsync("subscription-topic", subscription, cancellationToken);
        }
    }
}
