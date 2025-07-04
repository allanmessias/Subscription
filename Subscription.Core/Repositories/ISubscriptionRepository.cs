﻿using System;
using System.Threading;
using System.Threading.Tasks;
using Subscription.Core;
using SubscriptionModel = Subscription.Core.Domain.Entities.Subscription;


namespace Subscription.Core.Interfaces
{
    public interface ISubscriptionRepository
    {
        Task UpdateStatusAsync(SubscriptionModel subscription, CancellationToken cancellationToken = default);
        Task<SubscriptionModel> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        
        Task DeleteAsync (Guid subscriptionId, CancellationToken cancellationToken = default);

        Task CreateSubscriptionAsync(SubscriptionModel subscription, CancellationToken cancellationToken = default);
    }
}
