using System.ComponentModel.DataAnnotations;

namespace Subscription.Core.DTO

{
    public class User
    {
        [Key]
        public int Id {  get; set; }
        [Required]
        public string FullName {  get; set; }

        public DateTime CreatedAt { get; set; }

        public ICollection<Subscription> Subscriptions { get; set; }
    }
}
