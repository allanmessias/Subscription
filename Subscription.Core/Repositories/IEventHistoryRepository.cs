using System;
using System.Threading;
using System.Threading.Tasks;
using Subscription.Core.Domain.Entities;

namespace Subscription.Core.Interfaces
{
    public interface IEventHistoryRepository
    {
        Task AddAsync(EventHistory eventHistory, CancellationToken cancellationToken = default);
        Task<EventHistory> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
        
    }
}
