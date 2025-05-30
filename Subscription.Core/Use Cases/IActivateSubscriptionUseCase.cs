namespace Subscription.Core.Application
{
    public interface IActivateSubscriptionUseCase
    {
        Task Execute(Guid userId, Guid subscriptionId, CancellationToken cancellationToken = default);
    }
}
