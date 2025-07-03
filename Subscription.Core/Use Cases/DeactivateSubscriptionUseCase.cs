using Subscription.Core.Application;
using Subscription.Core.Domain;

namespace Subscription.Application.Use_Cases
{
    public class DeactivateSubscriptionUseCase : IDeactivateSubscriptionUseCase
    {
        private readonly IMessagePublisher _messagePublisher;

        public DeactivateSubscriptionUseCase(IMessagePublisher messagePublisher)
        {
            _messagePublisher = messagePublisher;
        }


        public async Task ExecuteAsync<T>(string routingKey, T message)
        {

            await _messagePublisher.PublishAsync(message, routingKey);
        }

        public Task ExecuteAsync<T>(Guid userId, Guid subscriptionId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task ExecuteAsync(Guid userId, Guid subscriptionId, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
