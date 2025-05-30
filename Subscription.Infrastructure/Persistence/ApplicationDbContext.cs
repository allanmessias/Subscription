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
        public DbSet<EventHistory> EventHistories { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasData(new User
                {
                    Id = new Guid("8d868b2a-df36-4a0e-bf40-7271c4f014f7"),
                    FullName = "Admin",
                    CreatedAt = new DateTime(2024, 04, 09, 12, 0, 0, DateTimeKind.Utc)
                });
            modelBuilder.Entity<SubscriptionModel>()
            .Property(s => s.Status)
            .HasConversion<string>();
        }
    }

}
