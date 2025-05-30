using Microsoft.EntityFrameworkCore;
using Subscription.Core.Domain.Entities;
using Subscription.Core.Interfaces;
using Subscription.Infrastructure.Persistence;

namespace Subscription.Infrastructure.Repositories
{
    public class EventHistoryRepository : IEventHistoryRepository
    {
        private readonly ApplicationDbContext _context;

        public EventHistoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(EventHistory history, CancellationToken cancellationToken = default)
        {
            await _context.EventHistories.AddAsync(history);
            await _context.SaveChangesAsync();
        }

        public Task<EventHistory> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("EventHistory ID cannot be empty.", nameof(id));
            }

            var eventHistory = _context.EventHistories
                .FirstOrDefaultAsync<EventHistory?>(e => e.Id == id, cancellationToken: cancellationToken);

            if (eventHistory is null)
            {
                throw new ArgumentException($"EventHistory with ID {id} was not found.", nameof(id));
            }

            return eventHistory;
        }
    }
}
