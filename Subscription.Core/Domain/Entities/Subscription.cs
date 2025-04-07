using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Subscription.Core.Domain.Entities
{
    public class Subscription
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public required User User { get; set; }
        [Required]
        public int StatusId { get; set; }
        [ForeignKey("StatusId")]
        public Status Status { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public DateTime? UpdatedAt { get; set; }
        public ICollection<EventHistory> EventHistories { get; set; } = new List<EventHistory>();
    }
}
