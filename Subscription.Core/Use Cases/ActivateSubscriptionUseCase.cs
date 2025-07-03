using Subscription.Application;
using Subscription.Core.Application;
using Subscription.Core.Domain;
using Subscription.Core.Domain.Entities;
using Subscription.Core.Interfaces;
using SubscriptionModel = Subscription.Core.Domain.Entities.Subscription;

namespace Subscription.Core.Use_Cases
{
    public class ActivateSubscriptionUseCase : IActivateSubscriptionUseCase
    {
        private readonly SubscriptionPublisher _messagePublisher;
        private readonly ISubscriptionRepository _repository;
        public ActivateSubscriptionUseCase(SubscriptionPublisher subscriptionPublisher, ISubscriptionRepository repository)
        {
            _messagePublisher = subscriptionPublisher;
            _repository = repository;
        }

        public async Task Execute(Guid userId, Guid subscriptionId, CancellationToken cancellationToken = default)
        {
            try
            {
                var existingSub = await _repository.GetByIdAsync(subscriptionId, cancellationToken);

                if (existingSub is not null)
                {
                    return;
                }

                var newSub = new SubscriptionModel
                {
                    Id = subscriptionId,
                    UserId = userId,
                    User = new User
                    {
                        Id = userId,
                    },
                    Status = Status.Active,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                await _repository.CreateSubscriptionAsync(newSub, cancellationToken);

                var request = new SubscriptionNotificationRequest
                {
                    UserId = userId,
                    SubscriptionId = subscriptionId,
                    NotificationType = SubscriptionNotificationType.SUBSCRIPTION_PURCHASED,
                    CreatedAt = newSub.CreatedAt,
                    UpdatedAt = (DateTime)newSub.UpdatedAt
                };

                await _messagePublisher.SendEventAsync(request, "subscription.ascan", cancellationToken);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
