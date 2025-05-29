using System.ComponentModel.DataAnnotations;

namespace Subscription.Core.Domain.Entities

{
    public class User
    {
        [Key]
        public Guid Id{  get; set; }
        [Required]
        public string FullName {  get; set; }

        public DateTime CreatedAt { get; set; }

        public ICollection<Subscription> Subscriptions { get; set; }
    }
}
