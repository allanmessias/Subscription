using Subscription.Core.Application;
using Subscription.Core.Domain;
using Subscription.Core.Domain.Entities;
using Subscription.Core.Interfaces;

namespace Subscription.Application.Use_Cases
{
    public class DeactivateSubscriptionUseCase : IDeactivateSubscriptionUseCase
    {
        private readonly SubscriptionPublisher _messagePublisher;
        private readonly ISubscriptionRepository _repository;
        private readonly IEventHistoryRepository _eventHistoryRepository;
        public DeactivateSubscriptionUseCase(SubscriptionPublisher subscriptionPublisher, ISubscriptionRepository repository, IEventHistoryRepository eventHistoryRepository)
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
                await _repository.DeleteAsync(subscriptionId, cancellationToken);
            }

            var newEventHistory = new EventHistory
            {
                Id = Guid.NewGuid(),
                SubscriptionId = subscriptionId,
                CreatedAt = DateTime.UtcNow,
                Type = "Canceled"
            };

            await _eventHistoryRepository.AddAsync(newEventHistory, cancellationToken);

            var request = new SubscriptionNotificationRequest
            {
                UserId = userId,
                SubscriptionId = subscriptionId,
                NotificationType = SubscriptionNotificationType.SUBSCRIPTION_CANCELED,
                UpdatedAt = DateTime.UtcNow,
            };

            await _messagePublisher.SendEventAsync(request, "subscription.ascan", cancellationToken);
        }

    }
}
