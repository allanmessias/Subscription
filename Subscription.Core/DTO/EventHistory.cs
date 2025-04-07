using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Subscription.Core.DTO
{
    public class EventHistory
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public Guid SubscriptionId { get; set; }
        [ForeignKey(nameof(SubscriptionId))]
        public Subscription Subscription { get; set; }
        [Required]
        public string Type { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
