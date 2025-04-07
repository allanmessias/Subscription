using Microsoft.EntityFrameworkCore;
using SubscriptionModel = Subscription.Core.Domain.Entities.Subscription;
using Subscription.Core.Domain.Entities;

namespace Subscription.Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<SubscriptionModel> Subscriptions { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<EventHistory> EventHistories { get; set; }
    }
}
