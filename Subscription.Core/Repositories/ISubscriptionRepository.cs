using System;
using System.Threading;
using System.Threading.Tasks;
using Subscription.Core;
using SubscriptionModel = Subscription.Core.DTO.Subscription; 


namespace Subscription.Core.Interfaces
{
    public interface ISubscriptionRepository
    {
        Task UpdateStatusAsync(Guid subscriptionId, string newStatus, CancellationToken cancellationToken = default);
        Task<SubscriptionModel> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        
        Task DeleteAsync (Guid subscriptionId, CancellationToken cancellationToken = default);

        Task AddAsync(SubscriptionModel subscription, CancellationToken cancellationToken = default);
    }
}
