using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Subscription.Core.Domain.Entities
{
    public class Subscription
    {
        [Key]
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; } = null!;

        [Required]
        public int StatusId { get; set; }

        [ForeignKey("StatusId")]
        public Status Status { get; set; } 

        [Required]
        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; } 

        public ICollection<EventHistory> EventHistories { get; set; } = new List<EventHistory>();
    }
}
