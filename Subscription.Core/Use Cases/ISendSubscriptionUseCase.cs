using Subscription.Core.Domain;

namespace Subscription.Core.Application
{
    public interface ISendSubscriptionUseCase
    {
        Task ExecuteAsync(SubscriptionUpdatedEvent subscription, CancellationToken cancellationToken = default);
    }
}
