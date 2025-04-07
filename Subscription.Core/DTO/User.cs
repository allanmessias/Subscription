namespace Subscription.Core.DTO

{
    public class User
    {
        public int Id {  get; set; }
        public string FullName {  get; set; }

        public DateTime CreatedAt { get; set; }

        public ICollection<Subscription> Subscriptions { get; set; }
    }
}
