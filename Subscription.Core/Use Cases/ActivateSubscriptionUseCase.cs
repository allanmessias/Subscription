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
        private readonly IEventHistoryRepository _eventHistoryRepository;
        public ActivateSubscriptionUseCase(SubscriptionPublisher subscriptionPublisher, ISubscriptionRepository repository, IEventHistoryRepository eventHistoryRepository      )
        {
            _messagePublisher = subscriptionPublisher;
            _repository = repository;
            _eventHistoryRepository = eventHistoryRepository;
        }

        public async Task Execute(Guid userId, Guid subscriptionId, CancellationToken cancellationToken = default)
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
                Status = Status.Active,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            var newEventHistory = new EventHistory
            {
                Id = Guid.NewGuid(),
                SubscriptionId = subscriptionId,
                CreatedAt = DateTime.UtcNow,
                Type = "Created"
            };

            await _repository.CreateSubscriptionAsync(newSub, cancellationToken);
            await _eventHistoryRepository.AddAsync(newEventHistory, cancellationToken);

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

    }
}
