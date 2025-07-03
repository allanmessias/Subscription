using Microsoft.EntityFrameworkCore;
using Subscription.Core.Interfaces;
using Subscription.Infrastructure.Persistence;
using Subscription.Core.Domain.Entities;
using SubscriptionModel = Subscription.Core.Domain.Entities.Subscription;

namespace Subscription.Infrastructure.Repositories
{
    class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly ApplicationDbContext _context;
        public SubscriptionRepository(ApplicationDbContext context) 
        {
            _context = context;
        }
            
        public async Task ActivateSubscriptionAsync(SubscriptionModel subscription, CancellationToken cancellationToken = default)
        {
            if (subscription is null)
            {
                throw new ArgumentNullException("Subscription cannot be null", nameof(subscription));
            }

            await _context.Subscriptions.AddAsync(subscription, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task CreateSubscriptionAsync(SubscriptionModel subscription, CancellationToken cancellationToken = default)
        {
            if (subscription is null)
            {
                throw new ArgumentNullException("Subscription cannot be null", nameof(subscription));
            }

            await _context.Subscriptions.AddAsync(subscription, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(Guid subscriptionId, CancellationToken cancellationToken = default)
        {
            var subscription = await _context.Subscriptions
                .FirstOrDefaultAsync<SubscriptionModel?>(s => s.Id == subscriptionId, cancellationToken: cancellationToken);
           
            if (subscription is null)
            {
                throw new KeyNotFoundException($"Subscription with ID {subscriptionId} was not found.");
            }

            _context.Subscriptions.Remove(subscription);
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task<SubscriptionModel?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Subscription ID cannot be empty.", nameof(id));
            }

            var subscription = await _context.Subscriptions
                .FirstOrDefaultAsync<SubscriptionModel?>(s => s.Id == id, cancellationToken: cancellationToken);

            return subscription; 
        }

        public async Task UpdateStatusAsync(SubscriptionModel subscription, CancellationToken cancellationToken = default)
        {
            if (subscription is null)
            {
                throw new ArgumentException("Subscription cannot be empty");
            }

            _context.Subscriptions.Update(subscription);
            await _context.SaveChangesAsync(cancellationToken);

        }
    }
}
