using Subscription.Application;
using Subscription.Core.Application;
using Subscription.Core.Domain;
using Subscription.Core.Domain.Entities;
using Subscription.Core.Interfaces;
using SubscriptionModel = Subscription.Core.Domain.Entities.Subscription;

namespace Subscription.Core.Use_Cases
{
    public class RestoreSubscriptionUseCase : IRestoreSubscriptionUseCase
    {
        private readonly SubscriptionPublisher _messagePublisher;
        private readonly ISubscriptionRepository _repository;
        private readonly IEventHistoryRepository _eventHistoryRepository;
        public RestoreSubscriptionUseCase(SubscriptionPublisher subscriptionPublisher, ISubscriptionRepository repository, IEventHistoryRepository eventHistoryRepository)
        {
            _messagePublisher = subscriptionPublisher;
            _repository = repository;
            _eventHistoryRepository = eventHistoryRepository;
        }

        public async Task Execute(Guid userId, Guid subscriptionId, CancellationToken cancellationToken = default)
        {
            var existingSub = await _repository.GetByIdAsync(subscriptionId, cancellationToken);

            if (existingSub is not null && existingSub.Status == Status.Canceled)
            {
                await _repository.UpdateStatusAsync(existingSub, cancellationToken);
            }

            var newEventHistory = new EventHistory
            {
                Id = Guid.NewGuid(),
                SubscriptionId = subscriptionId,
                CreatedAt = DateTime.UtcNow,
                Type = "Restarted"
            };

            await _eventHistoryRepository.AddAsync(newEventHistory, cancellationToken);

            var request = new SubscriptionNotificationRequest
            {
                UserId = userId,
                SubscriptionId = subscriptionId,
                NotificationType = SubscriptionNotificationType.SUBSCRIPTION_PURCHASED,
                UpdatedAt = DateTime.UtcNow,
            };

            await _messagePublisher.SendEventAsync(request, "subscription.ascan", cancellationToken);
        }

    }
}
