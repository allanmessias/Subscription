using Subscription.Core.Domain;

namespace Subscription.Core.Application
{
    public interface ISendSubscriptionUseCase
    {
        Task ExecuteAsync<T>(string routingKey, T message);
    }
}
